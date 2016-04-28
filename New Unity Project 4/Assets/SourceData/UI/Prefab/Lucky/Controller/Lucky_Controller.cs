using UnityEngine;
using System.Collections;

public class Lucky_Controller {

	private static Lucky_Controller _instance ;
	public static Lucky_Controller Instance(){
		if(_instance==null){
			_instance = new Lucky_Controller();
		}
		return _instance;
	}
	private Lucky_View _view;
	public void setChatView(Lucky_View value)
	{
			    _view = value;
	}
	private Lucky_Model _model;
	public Lucky_Model getModel(){
		if(_model==null){
			_model = new Lucky_Model();
		}
		return _model;
	}
	public Lucky_View GetView()
	{
		return this._view;
	}
}
