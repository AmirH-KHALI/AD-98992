#include <bits/stdc++.h>

using namespace std;

int k, t;
long ans = 0;
vector<string> inputs;
map<string, int> nameToIndex;
vector<set<int>> adj;

set<int> inVC;
set<int> outVC;

set<int> mark;

map<int, map<int, vector<set<int>>>> paths;

void buildGraph() {
    int counter = 0;
    for (auto str : inputs) {
        for (int i = 0; i + k - 1 < str.size(); ++i) {
            if (nameToIndex.find(str.substr(i, k - 1)) == nameToIndex.end()) {
                nameToIndex.insert( {str.substr(i, k - 1), counter++} );
                set<int> emptyVec;
                adj.push_back(emptyVec);
            }
            if (nameToIndex.find(str.substr(i + 1, k - 1)) == nameToIndex.end()) {
                nameToIndex.insert( {str.substr(i + 1, k - 1), counter++} );
                set<int> emptyVec;
                adj.push_back(emptyVec);
            }
            adj[nameToIndex[str.substr(i, k - 1)]].insert(nameToIndex[str.substr(i + 1, k - 1)]);
        }
    }
}

void findVC() {
    vector<int> inEdge(adj.size());
    vector<int> outEdge(adj.size());
    for (int i = 0; i < adj.size(); ++i) {
        for (auto v : adj[i]) {
            inEdge[v]++;
            outEdge[i]++;
        }
    }
    for (int i = 0; i < adj.size(); ++i) {
        if (inEdge[i] > 1) inVC.insert(i);
        if (outEdge[i] > 1) outVC.insert(i);
    }
}

void dfs (int root, int currentNode, set<int>& mark) {

    if (currentNode != root && inVC.find(currentNode) != inVC.end()) {
        set<int> path = mark;
        path.erase(currentNode);
        path.erase(root);
        paths[root][currentNode].push_back(path);
    }

    if(mark.size() > t) return;

    for (auto v : adj[currentNode]) {
        if (mark.find(v) == mark.end()) {
            set<int> marki = mark;
            marki.insert(v);
            dfs(root, v, marki);
        }
    }
}

int main () {
    ios::sync_with_stdio(false);

    cin >> k >> t;
    string s;
    while ((cin >> s) /**/&& (s != "TTGC")/**/) {
        inputs.push_back(s);
    }

    buildGraph();
    findVC();
    for (auto o : outVC) {
        mark.clear();
        mark.insert(o);
        dfs(o, o, mark);
    }
    for (auto uPaths : paths) {
        for (auto uTovPaths : uPaths.second) {//masir haye beyne u va v
            
            vector<set<int>> myPaths = uTovPaths.second;
            
            for (int i = 0; i < myPaths.size(); ++i) {
                for (int j = i + 1; j < myPaths.size(); ++j) { //be ezaye masir e i va j
                    
                    bool flag = true; //in do masir mostaqel hastand?
                    for (auto x : myPaths[i]) {
                        if (myPaths[j].find(x) != myPaths[j].end()) {
                            flag = false;
                            break;
                        }
                    }

                    if (flag) ans++;

                }
            }
        }
    }

    cout << ans << '\n';
}