# LootTableEditor-master

NOT BEING MAINTAINED ANYMORE
I made a replacement plugin with some more options
https://github.com/beerik94/LootControl


Works with TShock v4.5.14

Example config:

States:
- Normal
- Bloodmoon
- Eclipse
- Night
- Day
- Fullmoon

```json
{
  "1": { // NPC ID
    "drops": {
      "Normal": [ // STATE
        {
          "chance": 1.0, // Chance from 0.0 to 1.0 and 1.0 being 100%
          "low_stack": 50, // Lowest amount the amount will drop
          "high_stack": 99, // Highest amount the amount can drop
          "itemID": 73, // ItemID
          "prefix": 0 // Prefix ID
        },
		    {
          "chance": 1.0, // Chance from 0.0 to 1.0 and 1.0 being 100%
          "low_stack": 1, // Lowest amount the amount will drop
          "high_stack": 1, // Highest amount the amount can drop
          "itemID": 5000, // ItemID
          "prefix": 0 // Prefix ID
        }
      ],
      "Day": [ // STATE
        {
          "chance": 1.0, // Chance from 0.0 to 1.0 and 1.0 being 100%
          "low_stack": 50, // Lowest amount the amount will drop
          "high_stack": 99, // Highest amount the amount can drop
          "itemID": 73, // ItemID
          "prefix": 0 // Prefix ID
        },
		    {
          "chance": 1.0, // Chance from 0.0 to 1.0 and 1.0 being 100%
          "low_stack": 1, // Lowest amount the amount will drop
          "high_stack": 1, // Highest amount the amount can drop
          "itemID": 5000, // ItemID
          "prefix": 0 // Prefix ID
        }
      ]
    },
    "tryEachItem": true, // If this is set to "true" it will drop each item defined, if set to "false" it will stop after the first item
    "alsoDropDefaultLoot": false // If this is set to "true" it will also drop the normal loot defined by Terraria itself
  }
}
```

if you want to use this config make sure to remove all the // COMMENTS because JSON doesn't have comments
