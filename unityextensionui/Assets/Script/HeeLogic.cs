using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using System.Linq;

public class HeeLogic : MonoBehaviour
{
    public int _directionMax = 2;
    List<blockInfo> bMatrix = new List<blockInfo>();
    List<matchedBlock> bMatchedList = new List<matchedBlock>();
    //연산을 좀 더 부드럽게
    int toWest = -1;
    int toEast = 1;
    int toNorth = 1;
    int toSouth = -1;

    // Start is called before the first frame update
    void Start()
    {
        setBlockMatrix();
        var info = new searchInfo();
        info.origin = bMatrix.Where(x => x.x == 1 && x.y == 3).FirstOrDefault();

        info.origin = new blockInfo();
        Debug.Log("origin data : "+info.origin.eBlockType);
        info.startPos = info.origin;
        info.pathList = new List<blockInfo>();
        info.directionChangeCount = 0;
        info.eLastDirection = eDirection.none;

        searchBlock(info);

        foreach(var matched in bMatchedList)
        {
            Debug.Log("start x : "+matched.startBlockInfo.x+", start y : "+matched.startBlockInfo.y+",dest x :  "+matched.destBlockInfo.x+", dest y : "+matched.destBlockInfo.y);
        }
        Debug.Log("end search Block");
    }

    void setBlockMatrix()
    {
        for (var i = 0; i < 5; ++i)
        {
            for (var j = 0; j < 5; ++j)
            {
                blockInfo info = new blockInfo();
                info.x = i;
                info.y = j;
                info.eBlockType = EBlockType.none;
                if (i == 1 && j == 3) info.eBlockType = EBlockType.a;
                if (i == 4 && j == 2) info.eBlockType = EBlockType.a;
                bMatrix.Add(info);
            }
        }
    }

    void searchBlock(searchInfo info)
    {
        Debug.Log("input block start x : "+info.startPos.x+", y"+info.startPos.y);
        var origin = info.origin;
        var startPos = info.startPos;
        var pathList = info.pathList;

        var east = bMatrix.Where(x => x.x == startPos.x + toEast && x.y == startPos.y).Cast<blockInfo?>().FirstOrDefault();
        var west = bMatrix.Where(x => x.x == startPos.x + toWest && x.y == startPos.y).Cast<blockInfo?>().FirstOrDefault();
        var south = bMatrix.Where(x => x.x == startPos.x && x.y == startPos.y + toSouth).Cast<blockInfo?>().FirstOrDefault();
        var north = bMatrix.Where(x => x.x == startPos.x && x.y == startPos.y + toNorth).Cast<blockInfo?>().FirstOrDefault();

        if (west != null && !pathList.Contains((blockInfo)west))
        {
            var westData = (blockInfo)west;
            //검사 통과
            //같은 블럭인지 체크
            if (westData.eBlockType == origin.eBlockType)
            {
                Debug.Log("eblock from westData type : "+westData.eBlockType+", origin : "+origin.eBlockType+", x : "+westData.x+", y : "+westData.y);
                //더해줌
                var mBlock = bMatchedList.Where(x => x.startBlockInfo.x == origin.x && x.startBlockInfo.y == origin.y && x.destBlockInfo.x == westData.x && x.destBlockInfo.y == westData.y).FirstOrDefault();
                if(mBlock != null && pathList.Count + 1 < mBlock.path.Count )
                {
                    mBlock.path = pathList;
                    mBlock.path.Add(westData);
                }
            }
            else
            {
                if(info.eLastDirection == eDirection.west)
                {
                    //다른 블럭인데 lastdirection과 같을때 -> pathlist에 west를 추가하고 재귀 west를 startpos로 재귀 돌림
                    var sinfo = new searchInfo();
                    sinfo.startPos = westData;
                    sinfo.pathList = pathList;
                    sinfo.pathList.Add(westData);
                    sinfo.eLastDirection = eDirection.west;
                    sinfo.origin = info.origin;

                    searchBlock(sinfo);
                }
                else if(info.directionChangeCount + 1 <= _directionMax)
                {
                    //다른 블럭인데 lastdirection과 다르지만 directioncount + 1이 directionmax보다 작거나 같으면 pathlist에 west를 추가하고 재귀 west를 startpos로 재귀 돌림
                    var sinfo = new searchInfo();
                    sinfo.startPos = westData;
                    sinfo.pathList = pathList;
                    sinfo.pathList.Add(westData);
                    sinfo.directionChangeCount++;
                    sinfo.eLastDirection = eDirection.west;
                    sinfo.origin = info.origin;

                    searchBlock(sinfo);
                }
            }
        }

        if (east != null && !pathList.Contains((blockInfo)east))
        {
            //
            var eastData = (blockInfo)east;
            if (eastData.eBlockType == origin.eBlockType)
            {
                Debug.Log("eblock from eastData type : " + eastData.eBlockType + ", origin : " + origin.eBlockType + ", x : " + eastData.x + ", y : " + eastData.y);
                var mBlock = bMatchedList.Where(x => x.startBlockInfo.x == origin.x && x.startBlockInfo.y == origin.y && x.destBlockInfo.x == eastData.x && x.destBlockInfo.y == eastData.y).FirstOrDefault();
                if(mBlock != null && pathList.Count + 1 < mBlock.path.Count)
                {
                    mBlock.path = pathList;
                    mBlock.path.Add(eastData);
                }
            }
            else
            {
                if(info.eLastDirection == eDirection.east)
                {
                    var sinfo = new searchInfo();
                    sinfo.startPos = eastData;
                    sinfo.pathList = pathList;
                    sinfo.pathList.Add(eastData);
                    sinfo.eLastDirection = eDirection.east;
                    sinfo.origin = info.origin;

                    searchBlock(sinfo);
                }
                else if(info.directionChangeCount + 1 <= _directionMax)
                {
                    var sinfo = new searchInfo();
                    sinfo.startPos = eastData;
                    sinfo.pathList = pathList;
                    sinfo.pathList.Add(eastData);
                    sinfo.directionChangeCount++;
                    sinfo.eLastDirection = eDirection.east;
                    sinfo.origin = info.origin;

                    searchBlock(sinfo);
                }
            }
        }

        if (north != null && !pathList.Contains((blockInfo)north))
        {
            var northData = (blockInfo)north;
            if (northData.eBlockType == origin.eBlockType)
            {
                Debug.Log("eblock from northData type : " + northData.eBlockType + ", origin : " + origin.eBlockType + ", x : " + northData.x + ", y : " + northData.y);
                var mBlock = bMatchedList.Where(x => x.startBlockInfo.x == origin.x && x.startBlockInfo.y == origin.y && x.destBlockInfo.x == northData.x && x.destBlockInfo.y == northData.y).FirstOrDefault();
                if(mBlock != null && pathList.Count + 1 < mBlock.path.Count)
                {
                    mBlock.path = pathList;
                    mBlock.path.Add(northData);
                }
            }
            else
            {
                if (info.eLastDirection == eDirection.north)
                {
                    var sinfo = new searchInfo();
                    sinfo.startPos = northData;
                    sinfo.pathList = pathList;
                    sinfo.pathList.Add(northData);
                    sinfo.eLastDirection = eDirection.north;
                    sinfo.origin = info.origin;

                    searchBlock(sinfo);
                }
                else if(info.directionChangeCount + 1 <= _directionMax)
                {
                    var sinfo = new searchInfo();
                    sinfo.startPos = northData;
                    sinfo.pathList = pathList;
                    sinfo.pathList.Add(northData);
                    sinfo.directionChangeCount++;
                    sinfo.eLastDirection = eDirection.north;
                    sinfo.origin = info.origin;

                    searchBlock(sinfo);
                }
            }
        }

        if (south != null && !pathList.Contains((blockInfo)south))
        {
            var southData = (blockInfo)south;
            if (southData.eBlockType == origin.eBlockType)
            {
                Debug.Log("eblock from southData type : " + southData.eBlockType + ", origin : " + origin.eBlockType + ", x : " + southData.x + ", y : " + southData.y);
                var mBlock = bMatchedList.Where(x => x.startBlockInfo.x == origin.x && x.startBlockInfo.y == origin.y && x.destBlockInfo.x == southData.x && x.destBlockInfo.y == southData.y).FirstOrDefault();
                if(mBlock != null && pathList.Count + 1 < mBlock.path.Count)
                {
                    mBlock.path = pathList;
                    mBlock.path.Add(southData);
                }
            }
            else
            {
                if(info.eLastDirection == eDirection.south)
                {
                    var sinfo = new searchInfo();
                    sinfo.startPos = southData;
                    sinfo.pathList = pathList;
                    sinfo.pathList.Add(southData);
                    sinfo.eLastDirection = eDirection.north;
                    sinfo.origin = info.origin;

                    searchBlock(sinfo);
                }
                else if(info.directionChangeCount + 1 <= _directionMax)
                {
                    var sinfo = new searchInfo();
                    sinfo.startPos = southData;
                    sinfo.pathList = pathList;
                    sinfo.pathList.Add(southData);
                    sinfo.directionChangeCount++;
                    sinfo.eLastDirection = eDirection.south;
                    sinfo.origin = info.origin;

                    searchBlock(sinfo);
                }
            }
        }
    }

    void StartTask()
    {
        Task.Factory.StartNew(new Action<object>(pathfind), 1);
    }

    void pathfind(object i)
    {
        Debug.Log("start task i " + i);
        var t = int.Parse(i.ToString());
        if (t > 50) return;
        Task.Factory.StartNew(new Action<object>(pathfind), t+1);
        Debug.Log("end task i "+i);
    }
}

public class matchedBlock
{
    public blockInfo startBlockInfo;
    public blockInfo destBlockInfo;
    public List<blockInfo> path = new List<blockInfo>();
}

public class searchInfo
{
    public blockInfo origin;
    public blockInfo startPos;
    public int directionChangeCount;
    public eDirection eLastDirection;
    public List<blockInfo> pathList;
}

public struct blockInfo
{
    public int x;
    public int y;
    public EBlockType eBlockType;
}

public enum eDirection
{
    none,
    west,
    east,
    north,
    south,
}

public enum EBlockType
{
    none,
    wall,
    a,
    b,
    c,
    d,
    e,
    f,
}
