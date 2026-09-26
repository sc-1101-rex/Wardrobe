using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Wardrobe;

/// <summary>
/// An outfit struct that holds textures and texture offsets.
///
/// Create an outfit with a name outfitName, a texture outfitTexture, and an offset textureOffset.
/// </summary>
/// <param name="outfitName">The name of this outfit.</param>
/// <param name="outfitTexture">The texture this outfit holds.</param>
/// <param name="textureOffset">The amount the texture of this outfit offsets.</param>
public struct Outfit(string outfitName, Texture2D outfitTexture, Vector2 textureOffset)
{
    /// <summary>
    /// The name of the outfit.
    /// </summary>
    public string OutfitName { get; } = outfitName;
    
    /// <summary>
    /// The texture the outfit holds.
    /// </summary>
    public Texture2D OutfitTexture { get; } = outfitTexture;
    
    /// <summary>
    /// The amount the texture of this outfit offsets.
    /// </summary>
    public Vector2 TextureOffset { get; set; } = textureOffset;
}