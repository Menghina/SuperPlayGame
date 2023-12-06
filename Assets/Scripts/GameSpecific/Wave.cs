using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private bool isPlaying = false;

    public List<waveEvent> events = new List<waveEvent>();

    public void StartWave()
    {
        isPlaying = true;
        if (events.Count != 0)
        {
            events[0].StartEvent();
        }
        else
        {
            Debug.Log("Event ended");
            Destroy(this);
        }
    }

    private void Update()
    {
        if (!isPlaying)
        {
            return;
        }

        if (!events[0].RunEvent())
        {
            Debug.Log("End Wave");
            events.RemoveAt(0);// removes the used waveEvent and recounts the others from 0;
            if (events.Count == 0)
            {
                Debug.Log("End Wave");
                Destroy(this);
            }
            else
            {
                events[0].StartEvent();
            }
        }
    }
    [System.Serializable]
    public class waveEvent
    {
        public float duration = 15.0f;
        public List<SpawnInfo> spawnInfos;
        private float startTime;

        public void StartEvent()
        {
            startTime = Time.time;
        }

        public bool RunEvent()
        {
            if (duration == 0.0f && spawnInfos.Count == 0)
            {
                return false;
            }
            else if (Time.time - startTime > duration && duration != 0.0f)
            {
                return false;
            }

            for (int i = 0; i < spawnInfos.Count; i++)
            {
                spawnInfos[i].ReadyToSpawn();
                if (spawnInfos[i].ammount == 0)
                {
                    spawnInfos.RemoveAt(i);
                }
            }
            return true;
        }
        [System.Serializable]
        public class SpawnInfo
        {
            public int spawnPointIndex = 0;
            public int spawnPrefabIndex = 0;
            public float ammount = 10;
            public float interval = 1.0f;

            private float lastTime;

            public void ReadyToSpawn()
            {
                if (Time.time - lastTime >= interval)
                {
                    SpawnManager.Instance.Spawn(spawnPrefabIndex, spawnPointIndex);
                    ammount--;
                    lastTime = Time.time;
                }
            }
        }
    }
}
