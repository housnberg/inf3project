#pragma once
using namespace std;
class Node
{
public:
	Node(int id);
	void setPrev(Node* prev);
	void setDistance(int distance);
	void setVisited(bool visited);
	Node* getPrev();
	int getDistance();
	int getId();
	bool getVisited();

private:
	int id;
	int distance = 100000;
	bool visited = false;
	Node* prev;
	void setId(int id);

};