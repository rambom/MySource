package com.android.ringfly.actor;

import java.util.List;

public interface DemonBehaviour {
	// ����
	public void die();
	
	
	public void hitFail();

	// ����
	public void run(boolean ifShow);

	// ���˲ι�
	public void eat(List<AppleActor> actors);
	
	//��˸
	public void blink(int times);
	
	//��ͣ
	public void pause();
	
	//����
	public void goon();
	
}
