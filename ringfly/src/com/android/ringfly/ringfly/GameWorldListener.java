package com.android.ringfly.ringfly;

import java.util.List;

import com.android.ringfly.common.Assets;
import com.android.ringfly.common.Nature;
import com.badlogic.gdx.math.Vector2;

public interface GameWorldListener {
	/**
	 * ��ʼ
	 */
	public void onGameStart();

	/**
	 * ��ͣ
	 */
	public void onGamePause();

	/**
	 * ����
	 */
	public void onGameReset();

	/**
	 * ����
	 */
	public void onGameRuning();

	/**
	 * ����
	 */
	public void onContinue();

	/**
	 * ������ť
	 * 
	 */
	public void onSoundChange(Assets.Sounds sound);

	/**
	 * ����
	 */
	public void onDemonDie(String luckyNum, Integer magic, Integer gold);

	/**
	 * �۾�
	 */
	public void onEyeUsed();

	/**
	 * ����
	 */
	public void onShot(Nature nature);

	/**
	 * �߲�˵��ؿ�ѡ��
	 */
	public void onLevelSelect();

	/**
	 * �߲�˵����˵�ѡ��
	 */
	public void onMainMenuSelect();

	/**
	 * 
	 */
	public void onContactStone(List<Vector2> contactPoints);

	/**
	 * ����
	 */
	public void onGradeUp(Boolean blnPass, Boolean win);

	public void onGoldChanged(int gold);

	public void onMagicChanged(int magic);

}
