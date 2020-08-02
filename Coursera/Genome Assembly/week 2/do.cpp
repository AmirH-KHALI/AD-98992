#include<bits/stdc++.h>

using namespace std;

const int N = 1e4 + 20;
const int M = 1e5 + 20;

int n, m;
vector<pair<int, int>> edges;
vector<int> adj[N];
vector<int> edgeIndex[N];
vector<int> ans;
bool mark[M];

void dfs (int x) {
    for (int i = 0; i < adj[x].size(); ++i) {
        int child = adj[x][i];
        int edge = edgeIndex[x][i];
        if (!mark[edge]) {
            mark[edge] = true;
            dfs(child);
        }
    }
    ans.push_back(x);
}

int main () {
    cin >> n >> m;
    int inEdgesNum[N] = {};
    for (int i = 0; i < m; ++i) {
        int x, y;
        cin >> x >> y;
        adj[--x].push_back(--y);
        edgeIndex[x].push_back(edges.size());
        inEdgesNum[y]++;
        edges.push_back(make_pair(x, y));
    }
    for (int i = 0; i < n; ++i) {
        if (adj[i].size() != inEdgesNum[i]) {
            cout << 0 << '\n';
            return 0;
        }
    }
    dfs(0);
    cout << 1 << '\n';
    for (int i = 0; i < ans.size() - 1; ++i) {
        cout << ans[ans.size() - i - 1] + 1 << ' ';
    }
    return 0;
}