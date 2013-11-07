package com.ethan.mlife.dao;

import java.util.ArrayList;
import java.util.List;

import android.util.Log;

import com.ethan.mlife.MLifeApp;
import com.ethan.mlife.R;
import com.ethan.mlife.dao.impl.BusLineDaoImpl;
import com.ethan.mlife.dao.impl.BusStationDaoImpl;
import com.ethan.mlife.dao.impl.FavoriteBusDaoImpl;
import com.ethan.mlife.entity.FavoriteBus;
import com.ethan.mlife.entity.Line;
import com.ethan.mlife.entity.Station;

public abstract class AbstractBusDao implements BusDaoFacade {
	/**
	 * 公交收藏操作
	 */
	protected IFavoriteBusDao favoriteBusDao;
	/**
	 * 公交线路历史操作
	 */
	protected IBusLineDao busLineDao;
	/**
	 * 公交站台历史操作
	 */
	protected IBusStationDao busStationDao;
	/**
	 * url请求编码
	 */
	protected String urlEncode;

	protected AbstractBusDao() {
		favoriteBusDao = new FavoriteBusDaoImpl();
		busLineDao = new BusLineDaoImpl();
		busStationDao = new BusStationDaoImpl();
		urlEncode = MLifeApp.getContext().getString(R.string.httpRequestEncode);
	}

	public abstract List<Line> getBusLine(Line line);

	public abstract List<Station> getBusStation(Station station);

	public abstract List<Station> getLineStation(Line line);

	public abstract List<Line> getStationLine(Station station);

	public List<FavoriteBus> getFavoriteBus() {
		// TODO Auto-generated method stub
		try {
			FavoriteBus query = new FavoriteBus();
			query.setCityRegion(MyBusFactory.getMyBus().getRegion());
			query.setVisibility(FavoriteBus.VISIBLE);
			return this.favoriteBusDao.query(query, null, null,
					"click_count desc,update_time desc");
		} catch (Exception ex) {
			Log.e(AbstractBusDao.this.toString(), ex.toString());
		}
		return new ArrayList<FavoriteBus>();
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see com.android.mlife.dao.BusDaoFacade#saveToFavorite(java.util.List)
	 */
	public boolean saveToFavorite(List<FavoriteBus> listFavorite) {
		// TODO Auto-generated method stub
		boolean blnReturn = false;
		try {
			if (null != listFavorite && !listFavorite.isEmpty()
					&& listFavorite.size() > 0) {
				FavoriteBus query = new FavoriteBus();
				query.setCityRegion(MyBusFactory.getMyBus().getRegion());
				query.setFavoriteName(listFavorite.get(0).getFavoriteName());
				query.setBusType(listFavorite.get(0).getBusType());
				// 删除已存在的数据
				this.favoriteBusDao.delete(query);
			}

			for (FavoriteBus favorite : listFavorite) {
				favorite.setCityRegion(MyBusFactory.getMyBus().getRegion());
				// 添加收藏
				this.favoriteBusDao.insert(favorite);
				blnReturn = true;
			}
		} catch (Exception ex) {
			Log.e(AbstractBusDao.this.toString(), ex.toString());
		}
		return blnReturn;
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see
	 * com.android.mlife.dao.BusDaoFacade#getSwitchFavoriteBus(com.android.mlife
	 * .entity.FavoriteBus)
	 */
	public List<FavoriteBus> getSwitchFavoriteBus(FavoriteBus favorite) {
		// TODO Auto-generated method stub
		// TODO Auto-generated method stub
		try {
			FavoriteBus query = new FavoriteBus();
			query.setCityRegion(favorite.getCityRegion());
			query.setFavoriteName(favorite.getFavoriteName());
			query.setBusType(favorite.getBusType());
			// 找到直接返回
			return this.favoriteBusDao.query(query, null, null,
					"visibility desc");
		} catch (Exception ex) {
			Log.e(AbstractBusDao.this.toString(), ex.toString());
		}
		return new ArrayList<FavoriteBus>();
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see
	 * com.android.mlife.dao.BusDaoFacade#updateFavoriteBus(java.lang.String,
	 * com.android.mlife.entity.FavoriteBus)
	 */
	public boolean updateFavoriteBus(FavoriteBus where, FavoriteBus set) {
		// TODO Auto-generated method stub
		boolean blnReturn = false;
		try {
			this.favoriteBusDao.update(where, set);
			blnReturn = true;
		} catch (Exception ex) {
			Log.e(AbstractBusDao.this.toString(), ex.toString());
		}
		return blnReturn;
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see
	 * com.android.mlife.dao.BusDaoFacade#deleteFavoriteBus(com.android.mlife
	 * .entity.FavoriteBus)
	 */
	public boolean deleteFavoriteBus(FavoriteBus favorite) {
		// TODO Auto-generated method stub
		boolean blnReturn = false;
		try {
			this.favoriteBusDao.delete(favorite);
			blnReturn = true;
		} catch (Exception ex) {
			Log.e(AbstractBusDao.this.toString(), ex.toString());
		}
		return blnReturn;
	}

	/**
	 * 删除线路历史
	 * 
	 * @param line
	 * @return
	 */
	protected boolean deleteBusLine(Line line) {
		boolean blnReturn = false;
		try {
			this.busLineDao.delete(line);
			blnReturn = true;
		} catch (Exception ex) {
			Log.e(AbstractBusDao.this.toString(), ex.toString());
		}
		return blnReturn;
	}

	/**
	 * 删除站台历史
	 * 
	 * @param station
	 * @return
	 */
	protected boolean deleteBusStation(Station station) {
		boolean blnReturn = false;
		try {
			this.busStationDao.delete(station);
			blnReturn = true;
		} catch (Exception ex) {
			Log.e(AbstractBusDao.this.toString(), ex.toString());
		}
		return blnReturn;
	}

	/**
	 * 保存线路查询历史
	 * 
	 * @param line
	 * @return
	 */
	protected boolean saveBusLine(Line line) {
		boolean blnReturn = false;
		try {
			Line example = new Line();
			example.setCityRegion(MyBusFactory.getMyBus().getRegion());
			example.setId(line.getId());
			example.setLineNo(line.getLineNo());
			this.busLineDao.delete(example);
			line.setCityRegion(MyBusFactory.getMyBus().getRegion());
			this.busLineDao.insert(line);
			blnReturn = true;
		} catch (Exception ex) {
			Log.e(AbstractBusDao.this.toString(), ex.toString());
		}
		return blnReturn;
	}

	/**
	 * 保存站台查询历史
	 * 
	 * @param station
	 * @return
	 */
	protected boolean saveBusStation(Station station) {
		boolean blnReturn = false;
		try {
			Station example = new Station();
			example.setCityRegion(MyBusFactory.getMyBus().getRegion());
			example.setName(station.getName());
			example.setScode(station.getScode());
			this.busStationDao.delete(example);
			station.setCityRegion(MyBusFactory.getMyBus().getRegion());
			this.busStationDao.insert(station);
			blnReturn = true;
		} catch (Exception ex) {
			Log.e(AbstractBusDao.this.toString(), ex.toString());
		}
		return blnReturn;
	}

	protected List<Line> queryLineFromHis(Line line) {
		try {
			String strSql = String
					.format("city_Region='%s' and line_no like '%%%s%%' order by line_no",
							MyBusFactory.getMyBus().getRegion(),
							line.getLineNo(), line.getLineNo());
			return this.busLineDao.queryWithSql(strSql);
		} catch (Exception ex) {
			return new ArrayList<Line>();
		}
	}

	protected List<Station> queryStationFromHis(Station station) {
		try {
			String strSql = String.format(
					"city_Region='%s' and name like '%%%s%%' order by name",
					MyBusFactory.getMyBus().getRegion(), station.getName());
			return this.busStationDao.queryWithSql(strSql);
		} catch (Exception ex) {
			return new ArrayList<Station>();
		}
	}
}