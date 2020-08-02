#include <bits/stdc++.h>

using namespace std;

class Cube {
    public:

    string u;
    string l;
    string d;
    string r;
    int pos;

    Cube (string uu, string ll, string dd, string rr) {
        u = uu;
        l = ll;
        d = dd;
        r = rr;
        pos = -1;
    } 
    string toString() {
        return "(" + u + "," + l + "," + d + "," + r + ")";
    }
};

int n;
vector<Cube> cubes;
int table[36];

bool buildTable (int pos) {
    if (pos == n * n) {
        // cout << pos << '\n';
        return true;
    }

    // for (int i = 0; i < n * n; ++i) {
    //     cout << cubes[table[i]].toString();
    //     if (i % n != n - 1) cout << ";";
    //     else cout << "\n";
    // }

    for (int i = 0; i < cubes.size(); ++i) {
        Cube c = cubes[i];
        // cout << i << " " << c.pos << '\n';
        if (c.pos == -1) {
            // cout << "KHali\n";
            if (pos < n && c.u != "black") continue;
            // cout << "*bala meshki\n";
            if (pos % n == 0 && c.l != "black") continue;
            // cout << "*chap meshki\n";
            if (pos >= n * (n - 1) && c.d != "black") continue;
            // cout << "*paiin meshki\n";
            if (pos % n == n - 1 && c.r != "black") continue;
            // cout << "*rast meshki\n";
            if (pos >= n && c.u != cubes[table[pos - n]].d) continue;
            // cout << "*bala hamrang\n";
            if (pos % n != 0 && c.l != cubes[table[pos - 1]].r) continue;
            // cout << "*chap hamrang\n";
            c.pos = pos;
            table[pos] = i;
            if (buildTable(pos + 1))
                return true;
            c.pos = -1;
            table[pos] = -1;
        }
    }
    return false;
}

Cube parseToCube (string s) {
    int pos;
    s.erase(0, 1);
    string u = s.substr(0, pos = s.find(","));
    s.erase(0, pos + 1);
    string l = s.substr(0, pos = s.find(","));
    s.erase(0, pos + 1);
    string d = s.substr(0, pos = s.find(","));
    s.erase(0, pos + 1);
    string r = s.substr(0, pos = s.find(")"));

    return Cube(u,l,d,r);
} 

int main () {
    string s;
    while (/**cubes.size() < 9 &&/**/ (cin >> s))
    {
        cubes.push_back(parseToCube(s));
    }
    n = sqrt(cubes.size());
    for (int i = 0; i < n * n; ++i) {
        table[i] = -1;
    }
    buildTable(0);
    for (int i = 0; i < n * n; ++i) {
        cout << cubes[table[i]].toString();
        if (i % n != n - 1) cout << ";";
        else cout << "\n";
    }
}