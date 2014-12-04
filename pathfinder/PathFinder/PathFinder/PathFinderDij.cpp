#include "stdafx.h"
#include "Node.h"
#include <vector>
#include <iostream>
using namespace std;


namespace PathFinder {
	extern "C" {
		const int EDGEWEIGHT = 1;
		static const int MAXDISTANCE = 10000;
		vector<Node> nodes;

		__declspec(dllexport) int* findPath(int from, int to, int* map, int mapWidth, int mapHeight, int pathLength);

		int* givePath(Node* ref, int from, int pathLength);
		void calcDistance(Node* ref, int numb);


		int* findPath(int from, int to, int* map, int mapWidth, int mapHeight, int pathLength) {
			cout << "STARTED THE PATHFINDER";
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
			bool c = false;
			//sucht den Knoten mit der minimalen Distanz und speichert diesen als besucht
			while (!c) {
				int min = 100000;
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
				minNode->setVisited(true);

				//Ziel ist gefunden
				if (minNode->getId() == to) {
					return (givePath(minNode, from, pathLength));
				}

				//such die Nachbarknoten für den Knoten mit minimaler Distanz
				//befindet sich auf der Linken kante
				if (minNode->getId() % mapWidth == 0) {
					//befindet sich in der oberen linken ecke
					if (minNode->getId() == 0) {
						calcDistance(minNode, 1);
						calcDistance(minNode, mapWidth);
					}
					//Befindet sich an der unteren Linkten ecke
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
				//befindet sich auf der rechten kante
				else if (minNode->getId() % mapWidth == mapWidth - 1) {
					//befindet sich in der oberen rechten ecke
					if (minNode->getId() == mapWidth - 1) {
						calcDistance(minNode, -1);
						calcDistance(minNode, mapWidth);
					}
					//Befindet sich an der unteren rechten ecke
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
				//bfindet sich auf der unteren Kante
				else if (minNode->getId() > ((mapWidth*mapHeight) - mapWidth) && minNode->getId() < ((mapWidth*mapHeight) - 1)) {
					calcDistance(minNode, 1);
					calcDistance(minNode, -1);
					calcDistance(minNode, (-1)*mapWidth);
				}
				//befindet sich auf der oberen Kante
				else if (minNode->getId() > 0 && minNode->getId() < mapWidth - 1) {
					calcDistance(minNode, -1);
					calcDistance(minNode, 1);
					calcDistance(minNode, mapWidth);
				}
				//Befindet sich irgendwo in der mitte
				else {
					calcDistance(minNode, 1);
					calcDistance(minNode, -1);
					calcDistance(minNode, mapWidth);
					calcDistance(minNode, (-1)*mapWidth);
				}
			}
		}

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

		int* givePath(Node* ref, int from, int pathLength) {
			int* path = new int[pathLength];
			int count = 0;
			Node* prev = ref;
			cout << "FOUND THE FOLLOWING PATH (BACKWARDS)";
			while (prev->getId() != from) {
				//For test purposes only
				cout << "\n" << prev->getId();
				path[count] = prev->getId();
				prev = prev->getPrev();
			}
			return path;
		}

		void freeArray(int* pointer) {
			delete[] pointer;
		}
	}
}
