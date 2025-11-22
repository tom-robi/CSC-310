#include <iostream>
#include <set>
#include <chrono>
#include <random>
#include <cstdlib>
#include <fstream>

using namespace std;
using namespace std::chrono;

/*

    ChatGPT prompts used:

    In C++, write a program that creates a self-balancing binary search tree,
    and create a loop over n, where each iteration generates a random integer,
    inserts it into the tree, and records the time elapsed for each
    individual insert. For every 100000th insertion, write the index n
    and time required to a comma-delimited csv file.

    With an additional request to alter the code to produce results in nanoseconds.
*/

int main()
{
    ofstream outputFile("insertion_times.csv");
    if (!outputFile.is_open())
    {
        cerr << "Error: Unable to open the output file." << endl;
        return 1;
    }

    // Write header to CSV file
    outputFile << "Index,Time(ns)" << endl;

    const int n = 6000000; // Number of insertions
    set<int> tree;

    // Random number setup
    random_device rd;
    mt19937 gen(rd());
    uniform_int_distribution<int> dist;

    for (int i = 1; i <= n; ++i)
    {
        int randomInt = dist(gen); // Generate random integer
        auto start = high_resolution_clock::now();
        tree.insert(randomInt); // Insert into the tree
        auto end = high_resolution_clock::now();
        auto duration = duration_cast<nanoseconds>(end - start).count(); // Calculate time elapsed (nanoseconds)

        if (i % 1000 == 0)
        {
            // Write index and time to CSV file
            outputFile << i << "," << duration << endl;
        }
    }

    outputFile.close();
    cout << "Insertion complete. Results written to insertion_times.csv" << endl;

    return 0;
}
