// CommServer.Library.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include <string>


using namespace std;
namespace Library
{
	
	union Information
	{
		bool XMLUse;
		int EncryptionType;

	};
	Information cls;
	bool SetStyle(char Encryptiontype, bool XMLUse)
	{
		Information cl;
		cl.EncryptionType = Encryptiontype;
		cl.XMLUse = XMLUse;
		return false;
	}
	class DataReturn
	{
	private:
		string data;

	public:
		string dataReturn(string);

	};
	string DataReturn::dataReturn(string Data)
	{
		if (data == "GetServerStyle")
		{
			string parse = to_string(cls.EncryptionType) + ";" + to_string(cls.XMLUse);
			return parse;
		}
	}
}
