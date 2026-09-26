using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Wardrobe;

/// <summary>
/// A MonoGame State Machine boilerplate.
/// </summary>
public static class Wardrobe
{
    private static readonly Dictionary<string, int> ClosetsId = new();
    private static readonly Dictionary<int, Closet> Closets = new();
    
    /// <summary>
    /// Create a closet with name closetName and the array of outfits.
    /// </summary>
    /// <param name="closetName">The name of the closet.</param>
    /// <param name="outfits">The outfits to add to the closet.</param>
    public static void CreateCloset(string closetName, params Outfit[] outfits)
    {
        var currentClosetId = ClosetsId.Count;
        var closet = new Closet(currentClosetId, closetName, outfits);
        
        ClosetsId.Add(closetName, currentClosetId);
        Closets.Add(currentClosetId, closet);
    }

    /// <summary>
    /// Change the outfit of the closet named closetName into the outfit named outfitName.
    /// </summary>
    /// <param name="closetName">The name of the closet to change outfit.</param>
    /// <param name="outfitName">The name of the outfit to change into.</param>
    public static void ChangeOutfit(string closetName, string outfitName)
    {
        ref var closet = ref GetCloset(closetName);
        closet.ChangeOutfit(outfitName);
    }

    /// <summary>
    /// Draw the current outfit of the closet with name closetName with spriteBatch at worldPosition.
    /// </summary>
    /// <param name="spriteBatch">The sprite batch used to draw the outfit.</param>
    /// <param name="closetName">The name of the closet to draw.</param>
    /// <param name="worldPosition">The position to draw in the world.</param>
    public static void DrawOutfit(SpriteBatch spriteBatch, string closetName, Vector2 worldPosition)
    {
        ref var closet = ref GetCloset(closetName);
        var outfit = closet.ChosenOutfit;
        
        if (outfit.OutfitTexture == null)
            return;

        var drawPosition = worldPosition + outfit.TextureOffset;
        spriteBatch.Draw(outfit.OutfitTexture, drawPosition, Color.White);
    }
    
    /// <summary>
    /// Draw the outfits of all closets anchored at globalAnchorPosition.
    /// </summary>
    /// <param name="spriteBatch">The sprite batch used to draw the outfits.</param>
    /// <param name="globalAnchorPosition">The position to anchor in the world</param>
    public static void DrawAllOutfits(SpriteBatch spriteBatch, Vector2 globalAnchorPosition)
    {
        foreach (var (closetName, _) in ClosetsId)
        {
            ref var closet = ref GetCloset(closetName);
            var outfit = closet.ChosenOutfit;
            if (outfit.OutfitTexture == null)
                continue;
            
            var drawPosition = globalAnchorPosition + outfit.TextureOffset;
            spriteBatch.Draw(outfit.OutfitTexture, drawPosition, Color.White);
        }
    }

    /// <summary>
    /// Return the reference of the closet named closetName.
    /// </summary>
    /// <param name="closetName">The name of the closet.</param>
    /// <returns>The reference of the closet.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if closetName was not previously created.</exception>
    public static ref Closet GetCloset(string closetName)
    {
        if (!ClosetsId.TryGetValue(closetName, out var closetId))
            throw new KeyNotFoundException("The provided Closet Name: " + closetName +
                                           " does not correspond to an existing closet!");
        
        ref var closetToReturn =
            ref CollectionsMarshal.GetValueRefOrAddDefault(Closets, closetId, out var exists);
        if (!exists)
            throw new KeyNotFoundException("The provided Closet Id: " + closetId +
                                           " does not correspond to an existing closet!");
        
        return ref closetToReturn;
    }
}