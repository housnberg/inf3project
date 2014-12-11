#include "stdafx.h"
#include "Node.h"
#include <iostream>
#include <vector>
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
		throw new runtime_error("you cannot set a negative distance");
	}
}

void Node::setPrev(Node* prev)
{
	if (prev) {
		this->prev = prev;
	}
	else {
		throw new runtime_error("you cannot set a null neighbor");
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
		throw new runtime_error("you cannot set a negative distance");
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
		throw new runtime_error("you cannot set a null neighbor");
	}
}


vector<Node*>* Node::getNeighbors() {
	return &neighbors;
}