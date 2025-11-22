// https://www.geeksforgeeks.org/spell-checker-using-trie/
// C++ program to implement
// the above approach
#include <iostream>
#include <fstream>
#include <vector>
#include <string>
using namespace std;

// Structure of a Trie node
struct TrieNode {
    // Store address of a character
    TrieNode* Trie[26]; // Changed size to 26 for lowercase characters only

    // Check if the character is
    // last character of a string or not
    bool isEnd;

    // Constructor function
    TrieNode() {
        for (int i = 0; i < 26; i++) {
            Trie[i] = NULL;
        }
        isEnd = false;
    }
};

// Function to insert a string into Trie
void InsertTrie(TrieNode* root, string s) {
    TrieNode* temp = root;
    // Traverse the string, s
    for (int i = 0; i < s.length(); i++) {
        if (temp->Trie[s[i] - 'a'] == NULL) { // Changed index to reflect lowercase characters only
            // Initialize a node
            temp->Trie[s[i] - 'a'] = new TrieNode();
        }
        // Update temp
        temp = temp->Trie[s[i] - 'a'];
    }
    // Mark the last character of
    // the string to true
    temp->isEnd = true;
}

// Function to print suggestions of the string
void printSuggestions(TrieNode* root, string res) {
    // If current character is
    // the last character of a string
    if (root->isEnd == true) {
        cout << res << " ";
    }
    // Iterate over all possible
    // characters of the string
    for (int i = 0; i < 26; i++) { // Changed loop to reflect lowercase characters only
        // If current character
        // present in the Trie
        if (root->Trie[i] != NULL) {
            // Insert current character
            // into Trie
            res.push_back('a' + i); // Changed character to reflect lowercase characters only
            printSuggestions(root->Trie[i], res);
            res.pop_back();
        }
    }
}

// Function to check if the string
// is present in Trie or not
bool checkPresent(TrieNode* root, string key) {
    // Traverse the string
    for (int i = 0; i < key.length(); i++) {
        // If current character not
        // present in the Trie
        if (root->Trie[key[i] - 'a'] == NULL) { // Changed index to reflect lowercase characters only
            printSuggestions(root, key.substr(0, i));
            return false;
        }
        // Update root
        root = root->Trie[key[i] - 'a'];
    }
    if (root->isEnd == true) {
        return true;
    }
    printSuggestions(root, key);
    return false;
}

// Driver Code
int main() {
    // Initialize a Trie
    TrieNode* root = new TrieNode();
    
    // Read words from the text file and insert into Trie
    ifstream inputFile("aspell_dict_modified.txt");
    string word;
    while (inputFile >> word) {
        InsertTrie(root, word);
    }
    inputFile.close();

    // Get string key from user input
    string key;
    cout << "Enter a lowercase string for the key: ";
    cin >> key;

    // Check if the key is present in the Trie
    cout << endl;
    if (checkPresent(root, key)) {
        cout << endl << "Key Present: YES" << endl;
    } else {
        cout << endl << "Key Present: NO" << endl;
    }

    return 0;
}

