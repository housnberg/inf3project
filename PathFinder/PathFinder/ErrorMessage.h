#pragma once
#include <string>
#include <iostream>
using namespace std;

class ErrorMessage
{
public:
	static string errorMessage;
	static void throwErrorMessage(string message);
};

