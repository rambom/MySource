package com.android.ringfly.tools;

import com.badlogic.gdx.tools.imagepacker.TexturePacker;
import com.badlogic.gdx.tools.imagepacker.TexturePacker.Settings;

/**
 * ����ͼƬƴ�ӳ�һ��2��n�η��Ĵ�ͼ
 * 
 * @author fgshu
 * 
 */
public class PackTool {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		String pathIn = "D:\\Android\\ringFly\\rings";
		String pathOut = "D:\\Android\\ringFly\\ringsOut";

		String pathIn1 = "D:\\Android\\ringFly\\map";
		String pathOut1 = "D:\\Android\\ringFly\\mapOut";
		Settings settings = new Settings();
		settings.alias = true;
		TexturePacker.process(settings, pathIn, pathOut);
	}
}
