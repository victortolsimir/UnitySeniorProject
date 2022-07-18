using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDialogue : MonoBehaviour
{
    [SerializeField]
    private UniqueID storyID;

    [SerializeField]
    private ExclusionList exclusionList;

    [SerializeField]
    private List<string> textDialogue = new List<string>();

    [SerializeField]
    private List<AudioClip> audioDialogue = new List<AudioClip>();

    private GameObject player;
    private int currentDialogue = 0;
    private bool playingDialogue = false;

    private void Start()
    {
        if (textDialogue.Count < 1)
        {
            Debug.LogWarning("Missing Text Dialogue!");
        }

        if (audioDialogue.Count < 1)
        {
            Debug.LogWarning("Missing Audio Dialogue!");
        }

        if (exclusionList.exclusions.Contains(storyID))
            GetComponent<BoxCollider>().enabled = false;
    }

    private void Update()
    {
        if (playingDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(PlayDialogue());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<BoxCollider>().enabled = false;
            playingDialogue = true;
            player = other.gameObject;
            DisableMovement();
            exclusionList.Add(storyID);
            StartCoroutine(PlayDialogue());
        }
    }

    private void DisableMovement()
    {
        player.GetComponent<MyCharacterController>().DisableMovement();
    }

    private void EnableMovement()
    {
        player.GetComponent<MyCharacterController>().EnableMovement();
    }

    private IEnumerator PlayDialogue()
    {
        if (currentDialogue >= 0 && currentDialogue < textDialogue.Count)
        {
            yield return new WaitForSeconds(0.1f);

            MessagePanelSystem.ShowMessage(textDialogue[currentDialogue]);
            AmbientAudio.PlayAudioClipWithInterrupt(audioDialogue[currentDialogue++]);
        }
        else
        {
            playingDialogue = false;
            AmbientAudio.StopAudio();
            EnableMovement();
        }
    }
}
