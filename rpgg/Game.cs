using Godot;
using System;
using System.Xml.Linq;

public partial class Game : Node
{
    private int int_gamePlayerAtk = 200;               // プレイヤー基礎攻撃力
    private int int_totalAtk = 100;                          // 総攻撃力
    private int int_bossHp = 1000;                     // ボスHP
	private int int_bossMaxHp = 1000;
    private int int_totalScore = 0;                    // トータルスコア
    private int int_golfPlayerScore = 0;               // ゴルフプレイヤースコア
    private int int_gamePlayerScore = 0;               // ゲームプレイヤースコア
    private int int_count = 0;                         // プレイヤーの打数カウント
    private int int_attackCount = 0;                   // 攻撃回数カウント
    public void SumScore(int p_itemAtk)
    {
        int_count = int_count + 1;
        int_golfPlayerScore = int_count;                    // ゴルフプレイヤーの打数設定
        int int_itemAtk = int_gamePlayerAtk + p_itemAtk; 　 // プレイヤー基礎攻撃力 + アイテム攻撃力(引数)
        int_totalAtk = int_itemAtk;

        if (int_count != 1)
        {
            int_totalAtk = p_itemAtk + int_totalAtk; 　      // 総攻撃力 + アイテム攻撃力(引数)
        }
    }

    // 総攻撃力取得
    public int GetAttack()
    {
        // 総攻撃力を返却
        return int_totalAtk;
    }

    // 攻撃ボタン押下時
    public void Attack()
    {
        // 攻撃回数カウントUP
        int_attackCount += 1;
        // ボスにHPが残っている場合
        if (int_bossHp > 0)
        {
            // 残ボスHP
            int_bossHp -= int_totalAtk;
        }
    }

    // ボスのHP取得時
    public int GetEnemyHp()
    {
        // 残ボスのHPを返却
        return int_bossHp;
    }
	public int GetEnemyMaxHp(){
		return int_bossMaxHp;
	}

    //ボスの生死結果取得時
    public Boolean IsDead()
    {
        if (int_bossHp == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //最終のスコア結果取得時
    public (int TotalScore, int Count, int AttackCount) GetResult()
    {
        // ボスのHPが残っていた場合、ゴルフのスコアのみ返す
        if (int_bossHp > 0)
        {
            // ゲームの最終スコア、ゴルフの打数、攻撃の回数を返却
            return (int_totalScore, int_count, int_attackCount);
        }
        // ボスのHPが残っておらず、攻撃回数が５ターン以下だった場合
        else if (int_attackCount <= 5)
        {
            int_gamePlayerScore = 20;
            // 最終のスコアにゴルフプレイヤースコア - ゲームプレイヤースコアを設定
            int_totalScore = int_golfPlayerScore - int_gamePlayerScore;
            // ゲームの最終スコア、ゴルフの打数、攻撃の回数を返却
            return (int_totalScore, int_count, int_attackCount);
        }
        else
        {
            // ボスのHPが残っておらず、攻撃回数が５ターンより大きい場合
            int_totalScore = 10;
            int_totalScore = int_golfPlayerScore - int_gamePlayerScore;
            // ゲームの最終スコア、ゴルフの打数、攻撃の回数を返却
            return (int_totalScore, int_count, int_attackCount);
        }
    }
}