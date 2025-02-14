using System;
using System.Collections;
using UnityEngine;

public class EyeEvilEntity : CommonEntity {
    [SerializeField] private BaseDto dto;

    public EventHandler<HintEventArgs> onTakeHint;

}