#include <bits/stdc++.h>

using namespace std;

vector<string> inputs;

int eulerCycle(int k) {
    map<string, set<string>> degreeCounter;
    
    for (auto str : inputs) {
        for (int i = 0; i + k < str.size(); ++i) {
            degreeCounter[str.substr(i, k - 1)].insert(str.substr(i + 2, k - 1));
            if (i + k + 1 < str.size()) degreeCounter [str.substr(i + 2, k - 1)];
        }
    }

    for (auto dc : degreeCounter) {
        if (dc.second.empty()) return -1;
        if (dc.second.size() > 1) return 1;
    }
    return 0;
}

int main () {
    ios::sync_with_stdio(false);
    
    string s;
    while ((cin >> s) /**&& (s != "TGCA")/**/) {
        inputs.push_back(s);
    }

    int l = 0, r = 100;
    while (l <= r)
    {
        int m = (l + r) >> 1;
        int ec = eulerCycle(m);
        if (ec == -1) { r = m - 1; } 
        else if (ec == 0) { l = m; break; }
        else { l = m + 1; }
    }
    cout << l << '\n';
}