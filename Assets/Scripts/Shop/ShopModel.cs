using System.Collections.Generic;

public class ShopModel
{
    public int SelectedCharacterID { get; set; }
    public int CurrentCharacterID { get; set; }
    public List<Skin> AllSkins { get; } = new List<Skin>();
}