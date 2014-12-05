#include "stdafx.h"
#include "Node.h"
#include <vector>
#include <iostream>
using namespace std;


namespace PathFinder {
	extern "C" {
		const int EDGEWEIGHT = 1;
		static const int MAXDISTANCE = 100000;
		vector<Node> nodes;
		bool found = false;
		int* path;

		__declspec(dllexport) int* findPath(int from, int to, int* map, int mapWidth, int mapHeight, int pathLength);

		void givePath(Node* ref, int from, int pathLength);
		void calcDistance(Node* ref, int numb);

		//
		int* findPath(int from, int to, int* map, int mapWidth, int mapHeight, int pathLength) {
			cout << "ENTERED THE DLL AND STARTED THE PATHFINDER\n";
			if (from == to) {
				throw runtime_error("you cannot move here");
			}
			for (int i = 0; i < mapWidth*mapHeight; i++) {
				if (map[i] != 0) {
					if (i == from) {
						Node n(from);
						n.setDistance(0);
						nodes.push_back(n);
					}
					else {
						Node n(i);
						nodes.push_back(n);
					}
				}
				else {
					Node n(-1);
					nodes.push_back(n);
				}
			}
			while (!found) {
				int min = MAXDISTANCE;
				Node* minNode;
				for (Node n : nodes) {
					if (n.getId() != -1) {
						if (n.getDistance() < min) {
							if (!n.getVisited()) {
								minNode = &nodes[n.getId()];
								min = minNode->getDistance();
							}
						}
					}
				}
				if (minNode) {
					minNode->setVisited(true);
				}
				else {
					found = true;
					path = 0;
				}
				//found the destination point
				if (minNode->getId() == to) {
					found = true;
					givePath(minNode, from, pathLength);
				}
				else {
					//searches for the neigbour nodes
					//referenz point is located at the left edge
					if (minNode->getId() % mapWidth == 0) {
						//is located at the left upper corner 
						if (minNode->getId() == 0) {
							calcDistance(minNode, 1);
							calcDistance(minNode, mapWidth);
						}
						//is located at the left lower corner 
						else if (minNode->getId() == ((mapWidth*mapHeight) - mapWidth)) {
							calcDistance(minNode, 1);
							calcDistance(minNode, (-1)*mapWidth);
						}
						else {
							calcDistance(minNode, 1);
							calcDistance(minNode, (-1)*mapWidth);
							calcDistance(minNode, mapWidth);
						}
					}
					//is located at the right edge
					else if (minNode->getId() % mapWidth == mapWidth - 1) {
						//is located at the right upper corner 
						if (minNode->getId() == mapWidth - 1) {
							calcDistance(minNode, -1);
							calcDistance(minNode, mapWidth);
						}
						//is located at the right lower corner 
						else if (minNode->getId() == ((mapWidth*mapHeight) - 1)) {
							calcDistance(minNode, -1);
							calcDistance(minNode, -1);
						}
						else {
							calcDistance(minNode, -1);
							calcDistance(minNode, (-1)*mapWidth);
							calcDistance(minNode, mapWidth);
						}
					}
					//is located at the lower edge
					else if (minNode->getId() > ((mapWidth*mapHeight) - mapWidth) && minNode->getId() < ((mapWidth*mapHeight) - 1)) {
						calcDistance(minNode, 1);
						calcDistance(minNode, -1);
						calcDistance(minNode, (-1)*mapWidth);
					}
					//is located at the upper edge 
					else if (minNode->getId() > 0 && minNode->getId() < mapWidth - 1) {
						calcDistance(minNode, -1);
						calcDistance(minNode, 1);
						calcDistance(minNode, mapWidth);
					}
					//is located anywhere else
					else {
						calcDistance(minNode, 1);
						calcDistance(minNode, -1);
						calcDistance(minNode, mapWidth);
						calcDistance(minNode, (-1)*mapWidth);
					}
				}
			}
			return path;
		}

		//calculates the distance of two neighboring nodes
		void calcDistance(Node* ref, int numb) {
			Node* neighbor = &nodes[ref->getId() + numb];
			if (neighbor->getId() != -1) {
				if (!nodes[ref->getId() + numb].getVisited()) {
					int distance = (ref->getDistance() + EDGEWEIGHT);
					if (neighbor->getDistance() > distance) {
						neighbor->setDistance(distance);
						neighbor->setPrev(ref);
					}
				}
			}
		}

		//returns the whole path from start to destination point (backwards)
		//returns 
		void givePath(Node* ref, int from, int pathLength) {
			try {
				path = new int[pathLength];
				int count = 0;
				Node* prev = ref;
				cout << "FOUND THE FOLLOWING PATH (BACKWARDS)";
				while (prev->getId() != from) {
					//For test purposes only
					cout << "\n" << prev->getId();
					path[count] = prev->getId();
					prev = prev->getPrev();
				};
			}
			catch (exception) {
				path = 0;
			}
		}

		//frees the ram
		void freeArray(int* pointer) {
			delete[] pointer;
		}
	}
}
