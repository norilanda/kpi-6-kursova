﻿using DataGenerators.Generators;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Benchmark.Helpers;

public static class TreeGetter
{
    private static readonly Lazy<NodeState> _tree_2_3 = new(() => TreeStateGenerator.GenerateSymetricTree(2, 3));
    private static readonly Lazy<NodeState> _tree_2_22 = new(() => TreeStateGenerator.GenerateSymetricTree(2, 22));

    private static readonly Lazy<NodeState> _tree_5_3 = new(() => TreeStateGenerator.GenerateSymetricTree(5, 3));
    private static readonly Lazy<NodeState> _tree_5_5 = new(() => TreeStateGenerator.GenerateSymetricTree(5, 3));
    private static readonly Lazy<NodeState> _tree_5_8 = new(() => TreeStateGenerator.GenerateSymetricTree(5, 3));
    private static readonly Lazy<NodeState> _tree_5_10 = new(() => TreeStateGenerator.GenerateSymetricTree(5, 3));

    private static readonly Lazy<NodeState> _tree_10_3 = new(() => TreeStateGenerator.GenerateSymetricTree(10, 3));

    private static readonly Lazy<NodeState> _tree_100_3 = new(() => TreeStateGenerator.GenerateSymetricTree(100, 3));
    private static readonly Lazy<NodeState> _tree_250_3 = new(() => TreeStateGenerator.GenerateSymetricTree(250, 3));
    private static readonly Lazy<NodeState> _tree_500_3 = new(() => TreeStateGenerator.GenerateSymetricTree(500, 3));
    private static readonly Lazy<NodeState> _tree_700_3 = new(() => TreeStateGenerator.GenerateSymetricTree(700, 3));

    private static readonly Lazy<NodeState> _tree_3000_2 = new(() => TreeStateGenerator.GenerateSymetricTree(3000, 2));


    public static NodeState Tree_2_3 => _tree_2_3.Value;
    public static NodeState Tree_2_22 => _tree_2_22.Value;
    public static NodeState Tree_5_3 => _tree_5_3.Value;
    public static NodeState Tree_5_5 => _tree_5_5.Value;
    public static NodeState Tree_5_8 => _tree_5_8.Value;
    public static NodeState Tree_5_10 => _tree_5_10.Value;
    public static NodeState Tree_10_3 => _tree_10_3.Value;
    public static NodeState Tree_100_3 => _tree_100_3.Value;
    public static NodeState Tree_250_3 => _tree_250_3.Value;
    public static NodeState Tree_500_3 => _tree_500_3.Value;
    public static NodeState Tree_700_3 => _tree_700_3.Value;
    public static NodeState Tree_3000_2 => _tree_3000_2.Value;
}
