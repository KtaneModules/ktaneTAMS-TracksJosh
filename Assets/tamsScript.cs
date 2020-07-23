using UnityEngine;
using System.Collections;
using System.Linq;
using KModkit;
using Rnd = UnityEngine.Random;


public class tamsScript : MonoBehaviour

{
    public KMAudio audio;
    public KMBombModule bombModule;
    public Material[] arenaOptions;
    public Material[] arenaAnim;
    public Renderer arenaRender;
    public TextMesh arenaSubmits;

    private bool isActive;

    private int _arenas;
    private int _arenaSubmit;
    private int _arenaStages;
    private int _arenaStage1;
    private int _arenaStage2;
    private int _arenaStage3;
    private int _arenaStage4;


    private int _displayedArena;
    private int _displayedSubmit;
    private int _arenaIndex;
    private int _animationSequence;
    private int[] _arenaOrder;
    private int[] _arenaSubmitt;

    private static string[] _arenaNames = new[] { "Church Campgrounds", "Pyramid Temple", "Neon", "Cherry Feelings", "Picnic Area", "Antarctica", "The End", "Bungee", "Village", "Boat", "Stronghold", "Pokemon Arena", "Legendary Arena", "Rome", "Monument", "Beach", "Woodland Mansion", "Cave", "Mushrooms", "Nether", "Jungle", "Primal", "War Grounds", "Original", "Landfall's Development", "Advent Calendar", "Pevensey Castle", "Track and Field", "Dragon", "Dead Forest", "Mars", "Skirn-E", "Corruption", "Crimson", "Area 51" };
    private static string[] _submitNames = new[] { "Church Campgrounds", "Pyramid Temple", "Neon", "Cherry Feelings", "Picnic Area", "Antarctica", "The End", "Bungee", "Village", "Boat", "Stronghold", "Pokemon Arena", "Legendary Arena", "Rome", "Monument", "Beach", "Woodland Mansion", "Cave", "Mushrooms", "Nether", "Jungle", "Primal", "War Grounds", "Original", "Landfall's Development", "Advent Calendar", "Pevensey Castle", "Track and Field", "Dragon", "Dead Forest", "Mars", "Skirn-E", "Corruption", "Crimson", "Area 51" };

    public KMSelectable nextButton;
    public KMSelectable submitButton;
    public KMSelectable prevButton;


    private static int moduleIdCounter = 1;
    private int moduleId;
    private bool _isSolved;

    void Start()
    {
        
        moduleId = moduleIdCounter++;
        bombModule.OnActivate += Activate;
        _animationSequence = 0;
        PickArena();
        PickName();
        audio = GetComponent<KMAudio>();
        if (_arenaStages <= 4)
        {
            submitButton.OnInteract += delegate { Submit(); return false; };
            nextButton.OnInteract += delegate { Next(); return false; };
            prevButton.OnInteract += delegate { Prev(); return false; };
        }
        else
        {
            submitButton.OnInteract += delegate { Submit(); return true; };
            nextButton.OnInteract += delegate { Next(); return true; };
            prevButton.OnInteract += delegate { Prev(); return true; };
        }
    }

    // Submitting

    private void Submit()
    {
        audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        GetComponent<KMSelectable>().AddInteractionPunch();
        if (_arenaStages >= 4)
        {

        }
        else
        {
            if (arenaSubmits.text == _arenaNames[_arenaIndex])
            {
                Debug.LogFormat("[Totally Accurate Minecraft Simulator #{0}] Selected {1} on Stage {2}.", moduleId, arenaSubmits.text, _arenaStages + 1);
                if (_arenaStages == 3)
                {
                    _arenaStages = (_arenaStages + 54);
                    _arenaStage4 = _arenaIndex;
                    bombModule.HandlePass();
                    _isSolved = true;
                    TAMSAnimation();
                }
                else
                {
                    _displayedSubmit = 0;
                    _arenaStages = (_arenaStages + 1);
                    TAMSAnimate();
                }
            }
            else
            {
                Debug.LogFormat("[Totally Accurate Minecraft Simulator #{0}] STRIKE! Selected {1}! Resetting to Stage One.", moduleId, arenaSubmits.text);
                bombModule.HandleStrike();
                audio.PlaySoundAtTransform("laugh", transform);
                _displayedSubmit = 0;
                _arenaStages = (_arenaStages - 1) % 1;
                PickArena();
                PickName();
            }
        }
    }

    void Activate()
    {
        isActive = true;
    }

    void PickArena()
    {
        if (_arenaStages >= 4)
        {

        }
        else
        {
            _arenaOrder = Enumerable.Range(0, _arenaNames.Length).ToArray().Shuffle();
            _arenaIndex = Rnd.Range(0, 33);
            arenaRender.material = arenaOptions[_arenaIndex];
        }
    }

    void Next()
    {
        if (_arenaStages >= 4)
        {

        }
        else
        {
            audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            GetComponent<KMSelectable>().AddInteractionPunch();
            _displayedSubmit = (_displayedSubmit + 1) % _submitNames.Length;
            PrevUpdate();
        }

    }

    void Prev()
    {
        if (_arenaStages == 4)
        {

        }
        else
        {
            audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            GetComponent<KMSelectable>().AddInteractionPunch();
            _displayedSubmit = (_displayedSubmit + 34) % _submitNames.Length;
            PrevUpdate();
        }
        
    }


    void PrevUpdate()
    {
        arenaSubmits.text = _submitNames[_arenaSubmitt[_displayedSubmit]];
    }

    void PickName()
    {
        if (_arenaStages == 4)
        {

        }
        else
        {
            _arenaSubmitt = Enumerable.Range(0, _submitNames.Length).ToArray();
            _arenaSubmit = Rnd.Range(0, 1);
            arenaSubmits.text = _submitNames[_arenaSubmit];
            _arenaSubmit = _arenaSubmitt[_displayedSubmit];
        }

        if (_arenaStages == 0)
        {
            _arenaStage1 = _arenaIndex;
            Debug.LogFormat("[Totally Accurate Minecraft Simulator #{0}] Arena is {1} for Stage {2}.", moduleId, _arenaNames[_arenaStage1], _arenaStages + 1);
        }
        else if (_arenaStages == 1)
        {
            _arenaStage2 = _arenaIndex;
            Debug.LogFormat("[Totally Accurate Minecraft Simulator #{0}] Arena is {1} for Stage {2}.", moduleId, _arenaNames[_arenaStage2], _arenaStages + 1);
        }
        else if (_arenaStages == 2)
        {
            _arenaStage3 = _arenaIndex;
            Debug.LogFormat("[Totally Accurate Minecraft Simulator #{0}] Arena is {1} for Stage {2}.", moduleId, _arenaNames[_arenaStage3], _arenaStages + 1);
        }
        else
        {
            _arenaStage4 = _arenaIndex;
            Debug.LogFormat("[Totally Accurate Minecraft Simulator #{0}] Arena is {1} for Stage {2}.", moduleId, _arenaNames[_arenaStage4], _arenaStages + 1);
        }

    }
    // Animations

    void TAMSAnimate()
    {

        arenaRender.material = arenaAnim[_animationSequence];
        audio.PlaySoundAtTransform("beep", transform);
        Invoke("TamsAnimated", 1.5f);
    }

    void TAMSAnimation()
    {

        _animationSequence = (_animationSequence + 1);
        arenaRender.material = arenaAnim[_animationSequence];
        audio.PlaySoundAtTransform("beep", transform);
        Invoke("TamsAnim", 1.5f);
    }

    void TamsAnim()
    {
        _animationSequence = (_animationSequence + 1);
        arenaRender.material = arenaAnim[_animationSequence];
        audio.PlaySoundAtTransform("no", transform);
    }
    void TamsAnimated()
    {
        PickArena();
        PickName();
    }
    
    // Autosolver //

    IEnumerator TwitchHandleForcedSolve()
    {
        for (int i = 0; i < 4; i++)
        {
            while (arenaSubmits.text != _arenaNames[_arenaIndex])
            {
                nextButton.OnInteract();
                yield return new WaitForSeconds(0.05f);
                yield return true;
            }
            submitButton.OnInteract();
            yield return new WaitForSeconds(1.5f);
            TwitchHandleForcedSolve();

        }
    }
} 
