package com.android.jetman.entity;

public class SvnUser {
	private String name;
	private String password;
	public static final String tblName="user";	//�洢�ö���ı���
	public static final String tblColName="name";	//��һ����
	public static final String tblColPassword="password";	//�ڶ�����

	public SvnUser(String name, String pwd) {
		this.name = name;
		this.password = pwd;
	}
	public SvnUser()
	{		
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getPassword() {
		return password;
	}

	public void setPassword(String password) {
		this.password = password;
	}
}
