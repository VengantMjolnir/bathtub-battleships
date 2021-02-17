using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Scripts.Events;

public class GameEventTest : MonoBehaviour
{
    public Text InstructionsLabel;
    public Text[] ChoiceLabels;
    private GameEvent _GameEvent;

    public void Start()
    {
    }


    public void BeginEventTest(GameEventList eventList)
    {
        if (gameObject.active == false)
        {
            gameObject.SetActive(true);
        }

        _GameEvent = eventList.GetSelectedEvent();

        BeginEvent();
    }

    public void EndEventTest()
    {
        gameObject.SetActive(false);
    }

    private void BeginEvent()
    {
        if (_GameEvent.TextList.Count == 0)
        {
            InstructionsLabel.text = _GameEvent.Text;
        }
        else
        {
            int sel = Random.Range(0, _GameEvent.TextList.Count);
            InstructionsLabel.text = _GameEvent.TextList[sel];
        }

        int i = 0;
        if (_GameEvent.Choice != null)
        {
            for (i = 0; i < _GameEvent.Choice.Length; ++i)
            {
                GameEventChoice choice = _GameEvent.Choice[i];
                if (choice.TextList.Count == 0)
                {
                    ChoiceLabels[i].text = choice.Text;
                }
                else
                {
                    int sel = Random.Range(0, choice.TextList.Count);
                    ChoiceLabels[i].text = choice.TextList[sel];
                }
                SetChoiceLabelActive(i, true);
            }
        }
        else
        {
            ChoiceLabels[0].text = GameEventChoice.DefaultChoiceText;
        }

        // Turn off any choice labels we aren't using.
        for (i = (i == 0 ? 1 : i); i < ChoiceLabels.Length; ++i)
        {
            SetChoiceLabelActive(i, false);
        }
    }

    public void OnChoiceSelected(Text label)
    {
        int choiceIndex = -1;
        for (int i = 0; i < ChoiceLabels.Length; ++i)
        {
            if (ChoiceLabels[i] == label)
            {
                choiceIndex = i;
                break;
            }
        }

        GameEvent gameEvent = null;
        if (choiceIndex != -1)
        {
            if (_GameEvent.Choice != null)
            {
                GameEventChoice choice = _GameEvent.Choice[choiceIndex];
                if (choice != null && choice.InlineEvent != null)
                {
                    gameEvent = choice.InlineEvent;
                }
                else if (choice != null && choice.ReferenceEvent.ReferredEvent != null)
                {
                    gameEvent = choice.ReferenceEvent.ReferredEvent;
                }
            }
        }

        if (gameEvent != null)
        {
            _GameEvent = gameEvent;
            BeginEvent();
        }
        else
        {
            EndEventTest();
        }
    }

    public void SetChoiceLabelActive(int i, bool active)
    {
        if (i < ChoiceLabels.Length)
        {
            ChoiceLabels[i].gameObject.SetActive(active);
        }
    }
}
