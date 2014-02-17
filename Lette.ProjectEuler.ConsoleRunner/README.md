Project Euler Console Runner
----------------------------

Syntax:

    EulerRunner options
    
Options:

    [-p+ | --para[llel]]
        enables parallel execution (default)
        parallelism is limitied by number of available processors/cores

    [-p- | --seq[uential]]
        disables parallel execution
        no parallelism, solutions are not run in parallel

    [{-f | --filter} list]
        filters specific solutions by problem id
        list is a comma- and hyphen-separated list of problem ids

    [-r | --run] file-name
        path to a file containing solutions

Examples

    EulerRunner solutions.dll
        executes all solutions in solutions.dll in parallel

    EulerRunner solutions.dll -f 70,75-79 --seq
        executes solutions with problem id 70, 75, 76, 77, 78 and 79 (if found) in sequence

    EulerRunner --parallel --filter -10 solutions.dll
        executes all solutions in solutions.dll with problem id up to 10 in parallel
