// CommServer.Library.h

#pragma once

#include <string>

using namespace System;
using namespace std;

namespace CommServerLibrary {
	union Information
	{
		bool XMLUse;
		int EncryptionType;
		
	};
	
	Information cls;
	public ref class DataReturn
	{
	public:
		String^ dataReturn(String^);
		bool SetStyle(char, bool);

	};
	String^ DataReturn::dataReturn( String^ Data)
	{
		if (Data == "GetServerStyle")
		{
			String^ parse = cls.EncryptionType.ToString() + ";" + cls.XMLUse.ToString();
			return parse;
		}
	}
	bool DataReturn::SetStyle(char Encryptiontype, bool XMLUse)
	{
		Information cl;
		cl.EncryptionType = Encryptiontype;
		cl.XMLUse = XMLUse;
		return false;
	}
	String^ chk(String^ data)
	{
		
	}

	public ref class Class1
	{
		// TODO: Add your methods for this class here.
	};
}