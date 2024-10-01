using AbstractClass;

public interface IAttack {
	void RequestAttack();
	void TraceDamage();
	AbsController GetController();
	float GetAttackRange();
}
