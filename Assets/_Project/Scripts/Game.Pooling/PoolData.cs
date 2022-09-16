using UnityEngine;

namespace Game.Pooling
{
	[CreateAssetMenu(menuName = "PoolingService/PoolData", fileName = "NewPoolData")]
	public sealed class PoolData : ScriptableObject
	{
		public PoolType PoolType;
	
		public GameObject ObjectToPool;
	
		public int StartAmount;
	}
}
