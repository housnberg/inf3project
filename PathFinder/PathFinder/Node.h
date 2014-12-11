#pragma once
#include <vector>
#include <string>
#include <iostream>
using namespace std;

class Node
{
public:
	Node(int id, int distance);
	void setPrev(Node* prev);
	void setDistance(int distance);
	void setVisited(bool visited);
	Node* getPrev();
	int getDistance();
	int getId();
	bool getVisited();
	vector<Node*>* getNeighbors();
	void setNeighbor(Node* neighbor);

private:
	int id;
	int distance;
	bool visited = false;
	Node* prev;
	vector<Node*> neighbors;
	void setId(int id);
	void throwErrorMessage(string errorMessage);
};