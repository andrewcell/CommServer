// This is the main DLL file.

#include "stdafx.h"

#include "CommServer.Library.h"

using namespace System;
using namespace System::Data::SqlClient;

public ref class parseData {
private:
	int Login;
public:
	bool Parse(String^ data);

};
bool parseData::Parse(String^ data) {
	cli::array<String^>^ datas;
	datas = data->Split(';');
	if (datas[0] == "login") {

	}
	return false;
}

struct LoginInformation {

};

namespace CommSql {
	
	 ref struct Information {
		String^ id;
		String^ pw;
		String^ host;
		String^ dbname;
		int port;
	};
	

	public ref class MsSql {
	public:
		bool SetInformation(String^ id, String^ pw, String^ host, String^ dbname, int port);
		int chkConnect(Information infor);
	};
	bool MsSql::SetInformation(String^ id, String^ pw, String^ host, String^ dbname, int port) {
		Information infor;
		try {
			infor.id = id;
			infor.pw = pw;
			infor.host = host;
			infor.dbname = dbname;
			infor.port = port;
			return true;
		}
		catch (exception& e) {
			return false;
		}
		return false;
	}
	int MsSql::chkConnect(Information infor) {
		String^ query = "SELECT * FROM " + infor.dbname;
		String^ connStr = "server=" + infor.host + ";uid=" + infor.id + ";pwd=" + infor.pw + ";database=" + infor.dbname + ";";
		SqlConnection^ conn = gcnew SqlConnection(connStr);
		SqlCommand^ cmd = gcnew SqlCommand();
		cmd->Connection = conn;
		cmd->CommandText = query;
		conn->Open();
		SqlDataReader^ sdr = cmd->ExecuteReader();
		int cnt = 0;
		while (sdr->Read()) {
			cnt++;


		}
		return cnt;

	}
	
}