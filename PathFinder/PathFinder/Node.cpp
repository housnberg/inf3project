#include "stdafx.h"
#include "Node.h"
#include <iostream>
using namespace std;


Node::Node(int id)
{
	this->setId(id);

}

void Node::setDistance(int distance)
{
	this->distance = distance;
}

void Node::setPrev(Node* prev)
{
	this->prev = prev;
}

void Node::setVisited(bool visited)
{
	this->visited = visited;
}

void Node::setId(int id)
{
	this->id = id;
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