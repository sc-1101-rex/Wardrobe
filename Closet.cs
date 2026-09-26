using System.Runtime.InteropServices;

namespace Wardrobe;

/// <summary>
/// A closet struct that holds outfits.
/// </summary>
public struct Closet
{
    private readonly int _closetId;
    private readonly string _closetName;
    private int _chosenOutfitId;

    private readonly Dictionary<string, int> _closetOutfitsId;
    private readonly Dictionary<int, Outfit> _closetOutfits;

    /// <summary>
    /// The id of the closet.
    /// </summary>
    public int ClosetId => _closetId;
    
    /// <summary>
    /// The current chosen outfit of the closet.
    /// </summary>
    public Outfit ChosenOutfit => _closetOutfits[_chosenOutfitId];
    
    /// <summary>
    /// The dictionary of closet outfit ids.
    /// </summary>
    public Dictionary<string, int> ClosetOutfitsId => _closetOutfitsId;
    
    /// <summary>
    /// The dictinoary of closet outfits.
    /// </summary>
    public Dictionary<int, Outfit> ClosetOutfits => _closetOutfits;

    /// <summary>
    /// Create a closet with an int id closetId, name closetName, and stores the outfits.
    /// </summary>
    /// <param name="closetId">The id of this closet.</param>
    /// <param name="closetName">The name of this closet.</param>
    /// <param name="outfits">The outfits in this closet.</param>
    public Closet(int closetId, string closetName, params Outfit[] outfits)
    {
        _closetId = closetId;
        _closetName = closetName;
        _closetOutfitsId = new Dictionary<string, int>();
        _closetOutfits = new Dictionary<int, Outfit>();

        foreach (var outfit in outfits)
        {
            var currentOutfitId = _closetOutfits.Count;
            
            _closetOutfitsId.Add(outfit.OutfitName, currentOutfitId);
            _closetOutfits.Add(currentOutfitId, outfit);
        }

        _chosenOutfitId = 0;
    }

    /// <summary>
    /// Change the outfit of this closet into the outfit with name outfitName.
    /// </summary>
    /// <param name="outfitName">The name of the outfit for this closet to change into.</param>
    public void ChangeOutfit(string outfitName)
    {
        _chosenOutfitId = _closetOutfitsId[outfitName];
    }

    /// <summary>
    /// Return the reference of the outfit named outfitName.
    /// </summary>
    /// <param name="outfitName">The name of the outfit.</param>
    /// <returns>The reference of the outfit.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if outfitName was not previously created.</exception>
    public ref Outfit GetOutfit(string outfitName)
    {
        if (!_closetOutfitsId.TryGetValue(outfitName, out var outfitId))
            throw new KeyNotFoundException("The provided Outfit Name: " + outfitName +
                                           " does not correspond to an existing outfit!");
        
        ref var outfitToReturn =
            ref CollectionsMarshal.GetValueRefOrAddDefault(_closetOutfits, outfitId, out var exists);
        if (!exists)
            throw new KeyNotFoundException("The provided Outfit Id: " + outfitId +
                                           " does not correspond to an existing outfit!");
        
        return ref outfitToReturn;
    }
}