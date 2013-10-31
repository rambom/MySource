package com.android.jetman.data.dao;

import java.util.ArrayList;
import java.util.List;

import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import android.widget.Toast;

import com.android.jetman.JetmanApp;
import com.android.jetman.entity.AddressNode;
import com.android.jetman.util.HtmlParse;

/*
 * Address Book Of Dcjet
 * @author FGSHU
 */
public class Address {
	/**
	 * ��ѯ�õ�����ͨѶ��¼�б�
	 * 
	 * @return
	 */
	public List<AddressNode> getAllAddressNodes(String name, String pwd) {
		Document doc = HtmlParse.getHtmlDocument(name, pwd, "1");
		return parseDoc(doc);
	}

	/**
	 * ���ݲ�ѯ�����õ���ѯ����
	 * 
	 * @return
	 */
	public List<AddressNode> searchNodes(String where, String name, String pwd) {
		Document doc = HtmlParse.getHtmlDocument(name, pwd, where);
		return parseDoc(doc);
	}

	/*
	 * ��Document����ΪList<AddressNode>
	 */
	private List<AddressNode> parseDoc(Document doc) {
		List<AddressNode> listAddressNodes = new ArrayList<AddressNode>();
		if (null == doc)
			return null;
		if (doc.hasText() && doc.toString().contains("�û����������")) {
			Toast.makeText(JetmanApp.getContext(), "�û����������!",
					Toast.LENGTH_SHORT).show();
			return null;
		}
		Elements content = doc.getElementsByTag("table");
		Elements trs = content.get(0).getElementsByTag("tr");
		for (Element tr : trs) {
			AddressNode node = new AddressNode();
			node.setName(tr.child(0).text());
			node.setSex(tr.child(1).text());
			node.setPhone(tr.child(2).text());
			node.setMobile(tr.child(3).text());
			node.setEntphone(tr.child(4).text());
			node.setEmail(tr.child(5).text());
			listAddressNodes.add(node);
		}
		return listAddressNodes;
	}

}
