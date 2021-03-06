#include "stdafx.h"
#include "Node.h"
#include <vector>
#include <math.h>
#include <stdlib.h>
#include <iostream>
#include <string>
using namespace std;


namespace PathFinder {
	extern "C" {
		static const int EDGEWEIGHT = 1;
		static const int INFDISTANCE = 1000000;
		static const int MAXDISTANCE = 100000;
		static const int STARTDISTANCE = 0;
		vector<Node> nodes;
		bool found;
		int* path;

		__declspec(dllexport) int* findPath(int from, int to, int* map, int mapWidth, int mapHeight, int pathLength);
		__declspec(dllexport) void freeArray(int* pointer);
		void givePath(Node* ref, int from, int pathLength);
		void calcDistance(Node* ref, int numb);
		void findNeighbors(Node* ref, int numb, int mapWidth, int mapHeight);
		void initializeNodes(int from, int to, int* map, int mapSize);
		void throwErrorMessage(string errorMessage);

		/*
		DOKU
		switched the parameters to/from 
		*/
		int* findPath(int to, int from, int* map, int mapWidth, int mapHeight, int pathLength) {
			found = false;
			nodes.clear();
			cout << "ENTERED THE DLL AND STARTED THE PATHFINDER\n";
			if (from == to) {
				throwErrorMessage("you cannot move here");
			}
			if (map[from] == 0 || map[to] == 0) {
				throwErrorMessage("the field is not passable");
			}
			initializeNodes(from, to, map, mapWidth*mapHeight);
			for (Node node : nodes) {
				Node* ref = &nodes[node.getId()];
				findNeighbors(ref, 1, mapWidth, mapHeight);
				findNeighbors(ref, -1, mapWidth, mapHeight);
				findNeighbors(ref, mapWidth, mapWidth, mapHeight);
				findNeighbors(ref, (-1)*mapWidth, mapWidth, mapHeight);
			}
			while (!found) {
				int min = MAXDISTANCE;
				Node* minNode = 0; // equivalent to Node* minNode = nullptr
				for (Node tmp : nodes) {
					if (tmp.getDistance() < min) {
						if (!tmp.getVisited()) {
							minNode = &nodes[tmp.getId()];
							min = minNode->getDistance();
						}
					}
				}
				if (minNode) {
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
					throwErrorMessage("cannot find a valid path");
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
			cout << "FOUND THE FOLLOWING PATH (BACKWARDS)\n";
			while (prev->getId() != from) {
				count++;
				prev = prev->getPrev();
				path[count] = prev->getId();
			}
			path[0] = count;
			if (count > pathLength) {
				path = 0;
			}
		}

		//frees the ram
		void freeArray(int* pointer) {
			delete[] pointer;
		}

		void throwErrorMessage(string errorMessage) {
			string error = "RUNTIME ERROR: ";
			error += errorMessage + "\n";
			cout << error;
			throw new runtime_error(error);
		}
	}
}
