using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

public class DialogueNode
{
    public string NPCSpeech { get; set; }
    public List<string> PlayerAnswers { get; set; }
    [XmlIgnore]
    public Dictionary<string, DialogueNode> NextNodes { get; set; }

    [XmlArray("NextNodes")]
    [XmlArrayItem("NextNode")]
    public List<SerializableKeyValuePair> SerializableNextNodes { get; set; }

    public void PopulateNextNode()
    {
        // Populate the NextNodes dictionary from SerializableNextNodes
        foreach (var pair in SerializableNextNodes)
        {
            NextNodes[pair.Key] = pair.Value;
        }
    }

    public DialogueNode()
    {
        // Initialize properties
        PlayerAnswers = new List<string>();
        NextNodes = new Dictionary<string, DialogueNode>();
        SerializableNextNodes = new List<SerializableKeyValuePair>();
    }

    // Spatne pridani Node
    public void AddPlayerAnswer(string answer, DialogueNode nextNode)
    {
        // Add player answer and its linked node
        PlayerAnswers.Add(answer);
        NextNodes[answer] = nextNode;
        SerializableNextNodes.Add(new SerializableKeyValuePair(answer, nextNode));

    }
}

public class SerializableKeyValuePair
{
    [XmlAttribute]
    public string Key { get; set; }
    public DialogueNode Value { get; set; }

    public SerializableKeyValuePair()
    {
    }

    public SerializableKeyValuePair(string key, DialogueNode value)
    {
        // Initialize SerializableKeyValuePair
        Key = key;
        Value = value;
    }
}

public class DialogueManager
{
    static void Main(string[] args)
    {
        // Creating dialogue nodes
        DialogueNode node1 = new DialogueNode { NPCSpeech = "Hello there! How can I help you?" };
        DialogueNode node2 = new DialogueNode { NPCSpeech = "Great! What would you like to know?" };
        DialogueNode node3 = new DialogueNode { NPCSpeech = "I'm sorry, but you do not have access to shop yet :)" };
        DialogueNode node4 = new DialogueNode { NPCSpeech = "I'm sorry, I don't have any quests to give :(" };
        DialogueNode node5 = new DialogueNode { NPCSpeech = "Goodbye then!" };

        // Adding player answers and linking nodes
        node1.AddPlayerAnswer("Tell me about quests", node2);
        node1.AddPlayerAnswer("Ask about items", node3);
        node2.AddPlayerAnswer("Tell me about available quests", node4);
        node2.AddPlayerAnswer("Nevermind", node5);// Ends the conversation so that the player is not demanded to choose a answer

        // Saving dialogue to an XML file
        SaveDialogueToFile("dialogue.json", node1);

        // Loading dialogue from an XML file
        DialogueNode loadedNode = LoadDialogueFromFile("dialogue.json");
        loadedNode.PopulateNextNode();

        // Starting the conversation
        ConductConversation(loadedNode);
    }

    static void SaveDialogueToFile(string fileName, DialogueNode rootNode)
    {
        // Serializing dialogue node to an Jason file
        string serializer = JsonConvert.SerializeObject(rootNode, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(fileName, serializer);
    }

    static DialogueNode LoadDialogueFromFile(string fileName)
    {
        // Deserializing dialogue node from an Jason file
        string serializer = File.ReadAllText(fileName);
        return JsonConvert.DeserializeObject<DialogueNode>(serializer);    
    }

    static void ConductConversation(DialogueNode currentNode)
    {
        // Starting the conversation with the provided node
        while (currentNode != null)
        {
            Console.WriteLine("NPC: " + currentNode.NPCSpeech);

            if (currentNode.PlayerAnswers.Count > 0)
            {
                // Display player options
                Console.WriteLine("Player options:");
                for (int i = 0; i < currentNode.PlayerAnswers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {currentNode.PlayerAnswers[i]}");
                }

                // Player input for dialogue progression
                Console.Write("Your choice: ");
                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > currentNode.PlayerAnswers.Count)
                {
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    Console.Write("Your choice: ");
                }

                string selectedAnswer = currentNode.PlayerAnswers[choice - 1];
                if (currentNode.NextNodes.ContainsKey(selectedAnswer))
                {
                    currentNode = currentNode.NextNodes[selectedAnswer];
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                }
            }
            else
            {
                currentNode = null;
            }
        }
    }
}