
#include "stdafx.h"
#include <iostream>
#include <array>


class first{
private:	int mapw;
			int maph;
			int *map;



public: first(int para1, int para2, int *a){
	/*mapw = para1;
	maph = para2;*/
	map = new int[para1];

	std::cout << sizeof(a) / sizeof(a[0]);
	int length = sizeof(a) / sizeof(a[0]);


	for (int i = 0; i < length; i++){
		std::cout << "in Methode" << a[i] << "\n";
	}



	std::cout << "in Klasse";

}


};



int main(int argc, _TCHAR* argv[])
{
	int a[] = { 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4 };
	first f(2, 2, a);



	/*int map[10];
	for (int i = 0; i < sizeof(map) / sizeof(map[0]); i++){
	map[i] = i;
	std::cout << "Die Zahl an der Stelle:" << i << "lautet" << map[i] << "\n";
	*/




	std::cout << "\n";
	system("PAUSE");
	return 0;







}
