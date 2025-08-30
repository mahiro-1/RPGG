using Godot;
using System;

public partial class Presenter : Node2D
{
	MapManager mapManager;
	Location location;
	Game game;
	private int atk;
	Label atkLabel;
	Button shotOKButton;
	Button attackButton;
	Label enemyHpLabel;
	ProgressBar enemyHpBar;
	private int mapRatio = 15;
	Sprite2D course;
	TextEdit testX;
	TextEdit testY;
	Label clearLabel;
	Label scoreLabel;
	Sprite2D attackBackground;
	Sprite2D textBackground;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//クラスの初期化
		mapManager = new MapManager();
		location = new Location();
		game = new Game();

		//ノードの取得
		atkLabel = GetNode<Label>("../AtkLabel");
		enemyHpLabel = GetNode<Label>("../EnemyHpLabel");
		enemyHpBar = GetNode<ProgressBar>("../EnemyHpBar");
		course = GetNode<Sprite2D>("../Course");
		testX = GetNode<TextEdit>("../TestX");
		testY = GetNode<TextEdit>("../TestY");
		shotOKButton = GetNode<Button>("../ShotOK");
		attackButton = GetNode<Button>("../AttackButton");
		clearLabel = GetNode<Label>("../ClearLabel");
		scoreLabel = GetNode<Label>("../ScoreLabel");
		attackBackground = GetNode<Sprite2D>("../AttackBackground");
		textBackground = GetNode<Sprite2D>("../TextBackground");

		//ラベル情報の初期化
		//atkLabel.VisibleRatio = 0.5f;
		UpdateLabel();
		
		//ゴルフゲームの画面にする
		GolfGameStart();
		//GameClearStart();

		//ボタンのプレス処理
		shotOKButton.Pressed += ShotOKPressed;
		attackButton.Pressed += AttackPressed;
		
	}
	//ボタン押下で始まる処理たちーーーーーーーーーー
	private void ShotOKPressed(){
		shotOKButton.Hide();
		//Vector2 position = location.get_pos();
		Vector2  position = new Vector2(float.Parse(testX.Text),float.Parse(testY.Text));
		Cell nowCell = mapManager.get_cell(position.X, position.Y);
		moveCourse(nowCell);
		if(/*nowCell.is_green()*/true){
			BossFightStart();
		}
		else{
			game.SumScore(nowCell.gameEvent.baseAttack);
			UpdateLabel();
			shotOKButton.Show();
		}
	}
	private void AttackPressed(){
		attackButton.Hide();
		//攻撃処理
		game.Attack();
		UpdateBar();
		if(game.IsDead()){
			GameClearStart();
		}
		UpdateLabel();
		
		attackButton.Show();
	}
	//ーーーーーーーーーーーーーーーーーーーーーー

	//ゲーム画面切り替えの関数たちーーーーーーーー
	private void GolfGameStart(){
		//バトル画面の情報を隠す
		enemyHpBar.Hide();
		attackButton.Hide();
		enemyHpLabel.Hide();
		clearLabel.Hide();
		scoreLabel.Hide();
	}
	private void BossFightStart(){
		//ボス画面用のノードに表示を切り替える
		attackButton.Show();
		enemyHpLabel.Show();
		enemyHpBar.Show();

		shotOKButton.Hide();
		course.Hide();
		testX.Hide();
		testY.Hide();
	}
	private void GameClearStart(){
		clearLabel.Show();
		scoreLabel.Show();

		attackButton.Hide();
		enemyHpLabel.Hide();
		enemyHpBar.Hide();
		atkLabel.Hide();
		attackBackground.Hide();
		textBackground.Hide();
		shotOKButton.Hide();
		course.Hide();
		testX.Hide();
		testY.Hide();
		attackButton.Hide();

		var result = game.GetResult();
		scoreLabel.Text = "・ゴルフの打数\n　　　　　"+result.Count.ToString()+"打数\n・ボスにかかったターン数\n　　　　　"+result.AttackCount.ToString()+"ターン\n・トータル\n　　　　　"+result.TotalScore.ToString()+"点";
	}
	//ーーーーーーーーーーーーーーーーーーーー
	private void UpdateLabel(){
		atkLabel.Text = "攻撃力：" + game.GetAttack().ToString();
		//enemyHpLabel.Text = "敵のHP：" + game.GetEnemyHp().ToString();
	}
	private void UpdateBar(){
		enemyHpBar.Value = (float)(game.GetEnemyHp() / game.GetEnemyMaxHp()) * 100;
	}
	
	private void moveCourse(Cell nCell){
		Vector2 pos = new Vector2(448,224);//nCell.position;
		pos.X = 360 - pos.X * 15 + 512 * 15 * 0.5f;
		pos.Y = 360 - pos.Y * 15 + 256 * 15 * 0.5f;
		course.Position = pos;
	}

	//画面タップでその場所まで画面が移動するようにしたい
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventScreenTouch touch){
			GD.Print(touch.Position.X.ToString()+" "+touch.Position.Y.ToString());
			Vector2 pos = course.Position;
			pos.X += 360 - touch.Position.X;
			pos.Y += 360 - touch.Position.Y;
			course.Position = pos;
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
