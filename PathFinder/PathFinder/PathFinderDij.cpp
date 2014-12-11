#include "stdafx.h"
#include "Node.h"
#include <vector>
#include <math.h>
#include <stdlib.h>
#include <iostream>
using namespace std;


namespace PathFinder {
	extern "C" {
		static const int EDGEWEIGHT = 1;
		static const int INFDISTANCE = 1000000;
		static const int MAXDISTANCE = 100000;
		static const int STARTDISTANCE = 0;
		vector<Node> nodes;
		bool found = false;
		int* path;

		__declspec(dllexport) int* findPath(int from, int to, int* map, int mapWidth, int mapHeight, int pathLength);
		void givePath(Node* ref, int from, int pathLength);
		void calcDistance(Node* ref, int numb);
		void findNeighbors(Node* ref, int numb, int mapWidth, int mapHeight);
		void initializeNodes(int from, int to, int* map, int mapSize);

		int* findPath(int from, int to, int* map, int mapWidth, int mapHeight, int pathLength) {
			cout << "ENTERED THE DLL AND STARTED THE PATHFINDER\n";
			if (from == to) {
				throw runtime_error("you cannot move here");
			}
			if (map[from] == 0 || map[to] == 0) {
				throw new runtime_error("the field is not passable");
			}
			initializeNodes(from, to, map, mapWidth*mapHeight);
			for (Node node : nodes) {
				findNeighbors(&nodes[node.getId()], 1, mapWidth, mapHeight);
				findNeighbors(&nodes[node.getId()], -1, mapWidth, mapHeight);
				findNeighbors(&nodes[node.getId()], mapWidth, mapWidth, mapHeight);
				findNeighbors(&nodes[node.getId()], (-1)*mapWidth, mapWidth, mapHeight);
			}
			while (!found) {
				int min = MAXDISTANCE;
				Node* minNode;
				Node* tmp;
				for (unsigned int i = 0; i < nodes.size(); i++) {
					tmp = &nodes[i];
					if (tmp->getDistance() < INFDISTANCE) {
						if (tmp->getDistance() < min) {
							if (!tmp->getVisited()) {
								minNode = tmp;
								min = minNode->getDistance();
							}
						}
					}
				}
				if (minNode != nullptr) {
					minNode->setVisited(true);
					if (minNode->getId() == to) {
						cout << "-FOUND THE DESTINATION POINT\n";
						found = true;
						givePath(minNode, from, pathLength);
					}
					else {
						vector<Node*>* neighbors = minNode->getNeighbors();
						for (Node* node : *neighbors) {
							if (node->getDistance() < INFDISTANCE) {
								if (!node->getVisited()) {
									int distance = minNode->getDistance() + EDGEWEIGHT;
									if (node->getDistance() > distance) {
										node->setDistance(distance);
										node->setPrev(minNode);
									}
								}
							}
						}
					}
				}
				else {
					found = true;
					cout << "kein knotn vorhanden\n";
				}
			}
			return path;
		}

		//sets the neigbors of nodes
		void findNeighbors(Node* ref, int numb, int mapWidth, int mapHeight) {
			int neighborId = ref->getId() + numb;
			if (neighborId >= 0 && neighborId < mapWidth*mapHeight) {
				int diff = abs((ref->getId() % mapWidth) - (neighborId % mapWidth));
				if (diff == 0 || diff == 1) {
					Node* neighbor = &nodes[ref->getId() + numb];
					ref->setNeighbor(neighbor);
				}
			}
		}

		//creates the node objects
		void initializeNodes(int from, int to, int* map, int mapSize) {
			for (int i = 0; i < mapSize; i++) {
				if (map[i] != 0) {
					if (i == from) {
						Node n(from, STARTDISTANCE);
						nodes.push_back(n);
					}
					else {
						Node n(i, MAXDISTANCE);
						nodes.push_back(n);
					}
				}
				else {
					Node n(i, INFDISTANCE);
					nodes.push_back(n);
				}
			}
		}

		//calculate the whole path from start to destination point (backwards)
		void givePath(Node* ref, int from, int pathLength) {
			path = new int[pathLength];
			int count = 0;
			Node* prev = ref;
			cout << "FOUND THE FOLLOWING PATH (BACKWARDS)";
			while (prev->getId() != from) {
				//For test purposes only
				cout << "\n" << prev->getId();
				path[count] = prev->getId();
				prev = prev->getPrev();
				count++;
			}
			if (count > pathLength) {
				path = nullptr;
			}
		}

		//frees the ram
		void freeArray(int* pointer) {
			delete[] pointer;
		}
	}
}
