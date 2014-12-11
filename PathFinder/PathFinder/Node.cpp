#include "stdafx.h"
#include "Node.h"
using namespace std;


Node::Node(int id, int distance)
{
	this->setId(id);
	this->setDistance(distance);

}

void Node::setDistance(int distance)
{
	if (distance >= 0) {
		this->distance = distance;
	}
	else {
		throwErrorMessage("you cannot set a negative distance");
	}
}

void Node::setPrev(Node* prev)
{
	if (prev) {
		this->prev = prev;
	}
	else {
		throwErrorMessage("you cannot set a NULL previous node");
	}
}

void Node::setVisited(bool visited)
{
	this->visited = visited;
}

void Node::setId(int id)
{
	if (id >= 0) {
		this->id = id;
	}
	else {
		throwErrorMessage("you cannot set a negative id");
	}
}

int Node::getDistance()
{
	return this->distance;
}

Node* Node::getPrev()
{
	return prev;
}

int Node::getId()
{
	return id;
}

bool Node::getVisited()
{
	return this->visited;
}

void Node::setNeighbor(Node* neighbor) {
	if (neighbor) {
		this->neighbors.push_back(neighbor);
	}
	else {
		throwErrorMessage("you cannot set a NULL neighbor");
	}
}

vector<Node*>* Node::getNeighbors() {
	return &neighbors;
}

void Node::throwErrorMessage(string errorMessage) {
	string error = "RUNTIME ERROR: ";
	error += errorMessage + "\n";
	cout << error;
	throw new runtime_error(error);
}