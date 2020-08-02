#include <bits/stdc++.h>

using namespace std;

int calculateOverlap(string s1, string s2) {
    for (int i = 0; i < s1.size(); ++i) {
        if (s1.substr(i) == s2.substr(0, s1.size() - i)) {
            return s1.size() - i;
        }
    }
    return 0;
}

int main ()
{
    std::ios::sync_with_stdio(false);

    vector<string> inputs;
    string s;
    while ((cin >> s) /**&& (s != "TCG")/**/) {
        inputs.push_back(s);
    }

    bool mark[inputs.size()];
    int currentS = 0;
    string ans = "" + inputs[0];
    for (int i = 0; i < inputs.size(); ++i) {
        mark[currentS] = true;
        int bestIndex = -1;
        int bestOver = 0;
        for (int j = 0; j < inputs.size(); ++j) {
            if (!mark[j]) {
                int co = calculateOverlap(inputs[currentS], inputs[j]);
                if (co > bestOver) {
                    bestOver = co;
                    bestIndex = j;
                }
            }
        }
        if (bestIndex == -1) break;
        ans += inputs[bestIndex].substr(bestOver);
        currentS = bestIndex;
    }
    cout << ans.substr(calculateOverlap(inputs[currentS], inputs[0]));

    return 0;
}