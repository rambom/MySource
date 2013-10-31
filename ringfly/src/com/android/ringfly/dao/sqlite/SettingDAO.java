package com.android.ringfly.dao.sqlite;

import com.android.ringfly.common.Assets;

public interface SettingDAO {
	// ��������
	public void loadSetting();

	// ��������
	public void saveSetting();

	// ѭ�����ű�������
	public void backLoop(boolean on, Assets.Sounds sound);

	// �Ƴ���Ϸ
	public void finishGame();

	// ����
	public void stretchSound();

	public void scoreCountSound();

}
