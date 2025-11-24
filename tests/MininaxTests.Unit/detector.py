"""
Utilities for parsing `.trx` test result files (produced by `dotnet test`) and
spotting flaky tests.
"""

from __future__ import annotations

from collections import defaultdict
from pathlib import Path
from typing import DefaultDict, Dict, Iterable, List, Tuple
import xml.etree.ElementTree as ET


def _discover_namespace(root: ET.Element) -> Dict[str, str]:
    """
    Figure out the XML namespace used in the TRX file so we can write namespace
    aware queries. Returns a namespace mapping suitable for ElementTree lookups.
    """
    if root.tag.startswith("{") and "}" in root.tag:
        uri = root.tag[root.tag.find("{") + 1 : root.tag.find("}")]
        return {"t": uri}
    return {}


def iter_trx_files(root: Path | str = ".") -> Iterable[Path]:
    """Yield all `.trx` files under the given directory (non-recursive)."""
    base = Path(root)
    yield from base.glob("*.trx")


def parse_trx_file(trx_file: Path) -> Iterable[Tuple[str, str, Path]]:
    """
    Yield (test_name, outcome, source_file) tuples from a single TRX file.
    """
    tree = ET.parse(trx_file)
    root = tree.getroot()
    ns = _discover_namespace(root)

    # TRX structure:
    # <TestRun>
    #   <Results>
    #       <UnitTestResult testName="..." outcome="..." ... />
    #   </Results>
    # </TestRun>

    if ns:
        results_path = ".//t:UnitTestResult"
    else:
        results_path = ".//UnitTestResult"

    for result in root.findall(results_path, ns):
        name = result.attrib.get("testName", "").strip()
        outcome = result.attrib.get("outcome", "").strip()
        if not name:
            continue
        yield name, outcome, trx_file



def collect_test_outcomes(
    root: Path | str = ".",
) -> DefaultDict[str, List[Tuple[str, str]]]:
    """
    Walk all TRX files under `root` and collect outcomes per test name.

    Returns:
        A dict mapping test name -> list of (outcome, source_file) entries
        across all discovered runs.
    """
    outcomes: DefaultDict[str, List[Tuple[str, str]]] = defaultdict(list)
    for trx_file in iter_trx_files(root):
        for name, outcome, source in parse_trx_file(trx_file):
            outcomes[name].append((outcome, str(source)))
    return outcomes


def detect_flaky_tests(
    outcomes: Dict[str, List[Tuple[str, str]]],
) -> Dict[str, List[Tuple[str, str]]]:
    """
    Find tests that have mixed outcomes (both Passed and Failed) across runs.

    Args:
        outcomes: Mapping of test name -> list of (outcome, source_file)
                  pairs (e.g. from
                  `collect_test_outcomes`).

    Returns:
        A dictionary containing only the flaky tests with their outcome history.
    """
    flaky: Dict[str, List[Tuple[str, str]]] = {}
    for name, history in outcomes.items():
        states = {state.lower() for state, _ in history}
        if "passed" in states and "failed" in states:
            flaky[name] = history
    return flaky


if __name__ == "__main__":
    # Example usage: run `python detector.py` to print flaky tests in this folder.
    test_outcomes = collect_test_outcomes(".")
    flaky = detect_flaky_tests(test_outcomes)

    if not flaky:
        print("No flaky tests detected.")
    else:
        print("Flaky tests (outcome, source file):")
        for name, history in flaky.items():
            print(f"- {name}")
            for outcome, source in history:
                print(f"    {outcome}  ({source})")
