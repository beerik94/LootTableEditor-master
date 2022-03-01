using System;
using System.IO;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;

namespace LootTableEditor
{
	[ApiVersion(2, 1)]
	public class LootTableEditor : TerrariaPlugin
	{
		private Config config;
		private string path = "";
		public override string Author
		{
			get { return "Zack Piispanen"; }
		}

		public override string Description
		{
			get { return "Override vanilla npc loot tables"; }
		}

		public override string Name
		{
			get { return "NPC Loot table editor"; }
		}

		public override Version Version
		{
			get { return new Version(1, 4, 2, 3); }
		}

		public LootTableEditor(Main game)
			: base(game)
		{
			Order = 1;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				ServerApi.Hooks.NpcLootDrop.Deregister(this, OnLootDrop);
				GeneralHooks.ReloadEvent -= OnReload;
			}
		}

		public override void Initialize()
		{
			path = Path.Combine(TShock.SavePath, "LootDrop.json");
			config = new Config();
			config.ReadFile(path);
			ServerApi.Hooks.NpcLootDrop.Register(this, OnLootDrop);
			GeneralHooks.ReloadEvent += OnReload;
		}

		Random random = new Random();
		private void OnLootDrop(NpcLootDropEventArgs args)
		{
#if DEBUG
			Console.WriteLine("NPCID:{0}[NPCArrayIndex:{1}]: (X:{2}, Y:{3}) - Item:{4}", args.NpcId, args.NpcArrayIndex, args.Position.X, args.Position.Y, args.ItemId);
#endif
			
			if (config.LootReplacements.ContainsKey(args.NpcId))
			{
				DropReplacement repl = config.LootReplacements[args.NpcId];

				if (Main.bloodMoon && repl.drops.ContainsKey(State.Bloodmoon))
				{
#if DEBUG
							Console.WriteLine("LootTableEditor: BloodMoon Drops found.");
#endif
					
					foreach (Drop d in repl.drops[State.Bloodmoon])
					{
						double rng = random.NextDouble();
						if (d.chance >= rng)
						{
							var item = TShock.Utils.GetItemById(d.itemID);
							int stack = random.Next(d.low_stack, d.high_stack + 1);
							Item.NewItem(args.Source, (int)args.Position.X, (int)args.Position.Y, item.width, item.height, d.itemID, stack, args.Broadcast, d.prefix);
							
#if DEBUG
							Console.WriteLine("LootTableEditor: BloodMoonDrop ItemID:{0} - Amount:{1} - (X:{2}, Y:{3})", d.itemID, stack, args.Position.X, args.Position.Y);
#endif
							
							args.Handled = true;

							if (!repl.tryEachItem)
								break;
						}
					}
				}

				if (Main.eclipse && repl.drops.ContainsKey(State.Eclipse))
				{
#if DEBUG
					Console.WriteLine("LootTableEditor: Eclipse Drops found.");
#endif
					foreach (Drop d in repl.drops[State.Eclipse])
					{
						double rng = random.NextDouble();
						if (d.chance >= rng)
						{
							var item = TShock.Utils.GetItemById(d.itemID);
							int stack = random.Next(d.low_stack, d.high_stack + 1);
							Item.NewItem(args.Source, (int)args.Position.X, (int)args.Position.Y, item.width, item.height, d.itemID, stack, args.Broadcast, d.prefix);

#if DEBUG
							Console.WriteLine("LootTableEditor: EclipseDrop ItemID:{0} - Amount:{1} - (X:{2}, Y:{3})", d.itemID, stack, args.Position.X, args.Position.Y);
#endif
							
							args.Handled = true;

							if (!repl.tryEachItem)
								break;
						}
					}
				}

				if (Main.moonPhase == 0 && !Main.dayTime && repl.drops.ContainsKey(State.Fullmoon))
				{
#if DEBUG
					Console.WriteLine("LootTableEditor: Fullmoon Drops found.");
#endif
					foreach (Drop d in repl.drops[State.Fullmoon])
					{
						double rng = random.NextDouble();
						if (d.chance >= rng)
						{
							var item = TShock.Utils.GetItemById(d.itemID);
							int stack = random.Next(d.low_stack, d.high_stack + 1);
							Item.NewItem(args.Source, (int)args.Position.X, (int)args.Position.Y, item.width, item.height, d.itemID, stack, args.Broadcast, d.prefix);

#if DEBUG
							Console.WriteLine("LootTableEditor: FullmoonDrop ItemID:{0} - Amount:{1} - (X:{2}, Y:{3})", d.itemID, stack, args.Position.X, args.Position.Y);
#endif
							
							args.Handled = true;

							if (!repl.tryEachItem)
								break;
						}
					}
				}

				if (!Main.dayTime && repl.drops.ContainsKey(State.Night))
				{
#if DEBUG
					Console.WriteLine("LootTableEditor: Night Drops found.");
#endif
					foreach (Drop d in repl.drops[State.Night])
					{
						double rng = random.NextDouble();
						if (d.chance >= rng)
						{
							var item = TShock.Utils.GetItemById(d.itemID);
							int stack = random.Next(d.low_stack, d.high_stack + 1);
							Item.NewItem(args.Source, (int)args.Position.X, (int)args.Position.Y, item.width, item.height, d.itemID, stack, args.Broadcast, d.prefix);

#if DEBUG
							Console.WriteLine("LootTableEditor: NightDrop ItemID:{0} - Amount:{1} - (X:{2}, Y:{3})", d.itemID, stack, args.Position.X, args.Position.Y);
#endif
							
							args.Handled = true;

							if (!repl.tryEachItem)
								break;
						}
					}
				}

				if (Main.dayTime && repl.drops.ContainsKey(State.Day))
				{
#if DEBUG
					Console.WriteLine("LootTableEditor: Day Drops found.");
#endif
					foreach (Drop d in repl.drops[State.Day])
					{
						double rng = random.NextDouble();
						if (d.chance >= rng)
						{
							var item = TShock.Utils.GetItemById(d.itemID);
							int stack = random.Next(d.low_stack, d.high_stack + 1);
							Item.NewItem(args.Source, (int)args.Position.X, (int)args.Position.Y, item.width, item.height, d.itemID, stack, args.Broadcast, d.prefix);

#if DEBUG
							Console.WriteLine("LootTableEditor: DayDrop ItemID:{0} - Amount:{1} - (X:{2}, Y:{3})", d.itemID, stack, args.Position.X, args.Position.Y);
#endif
							
							args.Handled = true;

							if (!repl.tryEachItem)
								break;
						}
					}
				}

				if (repl.drops.ContainsKey(State.Normal))
				{
#if DEBUG
					Console.WriteLine("LootTableEditor: Normal Drops found.");
#endif
					foreach (Drop d in repl.drops[State.Normal])
					{
						double rng = random.NextDouble();
						if (d.chance >= rng)
						{
							var item = TShock.Utils.GetItemById(d.itemID);
							int stack = random.Next(d.low_stack, d.high_stack + 1);
							Item.NewItem(args.Source, (int)args.Position.X, (int)args.Position.Y, item.width, item.height, d.itemID, stack, args.Broadcast, d.prefix);
							args.Handled = true;
	
#if DEBUG
							Console.WriteLine("LootTableEditor: NormalDrop ItemID:{0} - Amount:{1} - (X:{2}, Y:{3})", d.itemID, stack, args.Position.X, args.Position.Y);
#endif
							
							if (!repl.tryEachItem)
								break;
						}
					}
				}

				if (!repl.alsoDropDefaultLoot)
					args.Handled = true;
			}
		}

		private void OnReload(ReloadEventArgs args)
		{
			config.ReadFile(path);
		}
	}
}
