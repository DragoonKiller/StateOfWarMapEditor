using System;
using UnityEngine;

using System.Collections.Generic;

using StateOfWarUtility;

namespace MapEditor
{
    public sealed class EdtDisplayer : MonoBehaviour
    {
        [SerializeField] Sprite playerBack;
        [SerializeField] Sprite enemyBack;
        [SerializeField] Sprite neutralBack;
        
        [SerializeField] Sprite[] sprites;
        
        
        public readonly Dictionary<BuildingType, Sprite> buildingPlayerSprites = new Dictionary<BuildingType, Sprite>();
        public readonly Dictionary<BuildingType, Sprite> buildingEnemySprites = new Dictionary<BuildingType, Sprite>();
        public readonly Dictionary<BuildingType, Sprite> buildingNeutralSprites = new Dictionary<BuildingType, Sprite>();
        
        public readonly Dictionary<UnitType, Sprite> unitPlayerSprites = new Dictionary<UnitType, Sprite>();
        public readonly Dictionary<UnitType, Sprite> unitEnemySprites = new Dictionary<UnitType, Sprite>();
        public readonly Dictionary<UnitType, Sprite> unitNeutralSprite = new Dictionary<UnitType, Sprite>();
        
        readonly List<SpriteRenderer> pool = new List<SpriteRenderer>();
        
        const int preserve = 5000;
        
        // ============================================================================================================
        // ============================================================================================================
        // ============================================================================================================
        
        void Start()
        {
            Preserve(preserve);
            PrepareSprites();
        }
        
        void Update()
        {
            if(Global.inst.edt != null)
                Refresh();
        }
        
        // ============================================================================================================
        // ============================================================================================================
        // ============================================================================================================
        
        void PrepareSprites()
        {
            // Hard coded file name and configuration file syntax.
            Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
            foreach(var i in this.sprites)
                sprites.Add(i.name, i);
            
            foreach(BuildingType bt in Enum.GetValues(typeof(BuildingType)))
                buildingPlayerSprites.Add(bt, sprites["P-" + bt.ToString()]);
            
            foreach(BuildingType bt in Enum.GetValues(typeof(BuildingType)))
                buildingEnemySprites.Add(bt, sprites["E-" + bt.ToString()]);
            
            foreach(BuildingType bt in Enum.GetValues(typeof(BuildingType)))
            {
                string n = "N-" + bt.ToString();
                if(sprites.ContainsKey(n))
                    buildingNeutralSprites.Add(bt, sprites[n]);
                else
                    buildingNeutralSprites.Add(bt, buildingEnemySprites[bt]);
            }
            
            foreach(UnitType bt in Enum.GetValues(typeof(UnitType)))
                if(!bt.IsProductionOnly())
                    unitPlayerSprites.Add(bt, sprites["P-" + bt.ToString()]);
                
            foreach(UnitType bt in Enum.GetValues(typeof(UnitType)))
                if(!bt.IsProductionOnly())
                    unitEnemySprites.Add(bt, sprites["E-" + bt.ToString()]);
            
            foreach(UnitType bt in Enum.GetValues(typeof(UnitType)))
            {
                if(!bt.IsProductionOnly())
                {
                    string n = "N-" + bt.ToString();
                    if(sprites.ContainsKey(n))
                        unitNeutralSprite.Add(bt, sprites[n]);
                    else
                        unitNeutralSprite.Add(bt, unitEnemySprites[bt]);
                }
            }
        }
        
        void Refresh()
        {
            var edt = Global.inst.edt;
            var gridSize = new Vector2(Global.gridSize, Global.gridSize);
            
            int decoNeeded = 0;
            foreach(var i in edt.buildings)
                decoNeeded += Global.inst.buildingSize[i.type].x * Global.inst.buildingSize[i.type].y;
            foreach(var i in edt.units)
                decoNeeded += 1;
            
            // Preserve for:
            // (1) Buildings and units.
            // (2) The background rectangle leading the selction and location.
            Preserve(edt.buildings.count + edt.units.count + decoNeeded);
            
            int cur = 0;
            
            // sorting order management:
            //   Decorations -> Buildings and Units -> Disk.
            // -- Negative --  --------- Positive ---------
            // In each seciton the order is sorted by Y coordinates, from top to bottom (bottom is visiable over top).
            const int maxMapHeight = 100;
            const int decoOrder = -maxMapHeight;
            const int unitsOrder = 1;
            const int diskOrder = maxMapHeight + 1;
            
            // Buildings...
            foreach(var b in edt.buildings)
            {
                var curOffset = new Vector2(Global.inst.buildingOffsets[b.type].x, -Global.inst.buildingOffsets[b.type].y);
                var rd = pool[cur++];
                rd.gameObject.SetActive(true);
                switch(b.owner)
                {
                    case Owner.Player: { rd.sprite = buildingPlayerSprites[b.type]; break; }
                    case Owner.Enemy: { rd.sprite = buildingEnemySprites[b.type]; break; }
                    case Owner.Neutral:
                    default: { rd.sprite = buildingNeutralSprites[b.type]; break; }
                }
                rd.gameObject.transform.position = Vector2.Scale(new Vector2(b.x, -b.y) - curOffset, gridSize);
                rd.sortingOrder = unitsOrder + Mathf.FloorToInt(b.y);
            }
            
            // Units...
            var unitOffset = new Vector2(Global.gridSize, -Global.gridSize) * 0.5f;
            foreach(var b in edt.units)
            {
                var curOffset = new Vector2(Global.inst.unitOffsets[b.type].x, -Global.inst.unitOffsets[b.type].y) * Global.gridSize + unitOffset;
                var rd = pool[cur++];
                rd.gameObject.SetActive(true);
                switch(b.owner)
                {
                    case Owner.Player: { rd.sprite = unitPlayerSprites[b.type]; break; }
                    case Owner.Enemy: { rd.sprite = unitEnemySprites[b.type]; break; }
                    case Owner.Neutral:
                    default: { rd.sprite = unitNeutralSprite[b.type]; break; }
                }
                rd.gameObject.transform.position = new Vector2(b.x, -b.y) - curOffset;
                rd.sortingOrder = unitsOrder + Mathf.FloorToInt(b.y);
            }
            
            // Building decorations...
            if(Global.inst.showDecoration)
            {
                foreach(var b in edt.buildings)
                {
                    for(int x=0; x<Global.inst.buildingSize[b.type].x; x++) for(int y=0; y<Global.inst.buildingSize[b.type].y; y++)
                    {
                        var rd = pool[cur++];
                        switch(b.owner)
                        {
                            case Owner.Player: { rd.sprite = playerBack; break; }
                            case Owner.Enemy: {  rd.sprite = enemyBack; break; }
                            case Owner.Neutral:
                            default: {  rd.sprite = neutralBack; break; }
                        }
                        rd.gameObject.transform.position = Vector2.Scale(new Vector2(b.x + x, -b.y - y), gridSize);
                        rd.sortingOrder = decoOrder + Mathf.FloorToInt(b.y + y);
                        rd.gameObject.SetActive(true);
                    }
                }
                
                // Unit decorations...
                foreach(var b in edt.units)
                {
                    var rd = pool[cur++];
                    rd.gameObject.SetActive(true);
                    switch(b.owner)
                    {
                        case Owner.Player: { rd.sprite = playerBack; break; }
                        case Owner.Enemy: {  rd.sprite = enemyBack; break; }
                        case Owner.Neutral:
                        default: {  rd.sprite = neutralBack; break; }
                    }
                    rd.gameObject.transform.position = new Vector2(b.x, -b.y) - unitOffset;
                    if(b.type == UnitType.Disk)
                        rd.sortingOrder = diskOrder;
                    else
                        rd.sortingOrder = decoOrder + Mathf.FloorToInt(b.y);
                }
            }
            
            while(cur < pool.Count)
            {
                pool[cur].gameObject.SetActive(false);
                cur++;
            }
        }
        
        void Preserve(int cnt)
        {
            while(cnt > pool.Count)
            {
                var x = new GameObject();
                x.transform.SetParent(this.transform);
                var rd = x.AddComponent<SpriteRenderer>();
                rd.sortingLayerName = "Units";
                pool.Add(rd);
                x.SetActive(false);
            }
        }
    }
}
