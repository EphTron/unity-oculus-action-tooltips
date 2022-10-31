// VRSYS plugin of Virtual Reality and Visualization Research Group (Bauhaus University Weimar)
//  _    ______  _______  _______
// | |  / / __ \/ ___/\ \/ / ___/
// | | / / /_/ /\__ \  \  /\__ \ 
// | |/ / _, _/___/ /  / /___/ / 
// |___/_/ |_|/____/  /_//____/  
//
//  __                            __                       __   __   __    ___ .  . ___
// |__)  /\  |  | |__|  /\  |  | /__`    |  | |\ | | \  / |__  |__) /__` |  |   /\   |  
// |__) /~~\ \__/ |  | /~~\ \__/ .__/    \__/ | \| |  \/  |___ |  \ .__/ |  |  /~~\  |  
//
//       ___               __                                                           
// |  | |__  |  |\/|  /\  |__)                                                          
// |/\| |___ |  |  | /~~\ |  \                                                                                                                                                                                     
//
// Copyright (c) 2022 Virtual Reality and Visualization Research Group
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//-----------------------------------------------------------------
//   Authors:        Ephraim Schott
//   Date:           2022
//-----------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TooltipHandler : MonoBehaviour
{
    public TooltipMapper leftTooltipMapper;
    public TooltipMapper rightTooltipMapper;

    public InputActionProperty toggleTooptipsAction;

    public bool hasTooltips = false;
    public Tooltip toggleTooltip;

    public bool showTooltips { get; private set; } = true;

    public Dictionary<string, Tooltip> tooltips = new Dictionary<string, Tooltip>();

    // Start is called before the first frame update
    void Start()
    {
        InitializeTooltips();
    }

    // Update is called once per frame
    void Update()
    {
        if (toggleTooptipsAction.action.WasReleasedThisFrame() && showTooltips)
        {
            showTooltips = false;
            leftTooltipMapper.isActive = false;
            rightTooltipMapper.isActive = false;
            HideAllTooltips();
            this.HideTooltip(toggleTooltip);
        } else if (toggleTooptipsAction.action.WasReleasedThisFrame() && !showTooltips)
        {
            showTooltips = true;
            leftTooltipMapper.isActive = true;
            rightTooltipMapper.isActive = true;
            ShowAllTooltips();
            this.ShowTooltip(toggleTooltip, add: true);
        }
    }

    void InitializeTooltips()
    {
        if (hasTooltips && showTooltips)
        {
            this.ShowTooltip(toggleTooltip);
        }
    }

    public void ShowAllTooltips()
    {
        if (!showTooltips)
            return;

        foreach (var tPair in tooltips)
        {
            if (tPair.Value.hand == TooltipHand.Left)
            {
                leftTooltipMapper.ShowTooltip(tPair.Value);
            } else if (tPair.Value.hand == TooltipHand.Right)
            {
                rightTooltipMapper.ShowTooltip(tPair.Value);
            }
            else if (tPair.Value.hand == TooltipHand.Both)
            {
                leftTooltipMapper.ShowTooltip(tPair.Value);
                rightTooltipMapper.ShowTooltip(tPair.Value);
            }
        }
    }

    public void HideAllTooltips()
    {
        foreach (var tPair in tooltips)
        {
            if (tPair.Value.hand == TooltipHand.Left)
            {
                leftTooltipMapper.HideTooltip(tPair.Value);
            }
            else if (tPair.Value.hand == TooltipHand.Right)
            {
                rightTooltipMapper.HideTooltip(tPair.Value);
            }
            else if (tPair.Value.hand == TooltipHand.Both)
            {
                leftTooltipMapper.HideTooltip(tPair.Value);
                rightTooltipMapper.HideTooltip(tPair.Value);
            }
        }
    }

    public void ShowTooltip(Tooltip tooltip, bool add=false)
    {
        if (add)
        {
            if (!tooltips.ContainsKey(tooltip.tooltipName))
            {
                tooltips.Add(tooltip.tooltipName, tooltip);
            }
            else
            {
                tooltips.Remove(tooltip.tooltipName);
                tooltips.Add(tooltip.tooltipName, tooltip);
            }
        }

        if (!showTooltips)
            return;

        if (tooltip.hand == TooltipHand.Left)
        {
            leftTooltipMapper.ShowTooltip(tooltip);
        } else if (tooltip.hand == TooltipHand.Right)
        {
            rightTooltipMapper.ShowTooltip(tooltip);
        }
        else if (tooltip.hand == TooltipHand.Both)
        {
            leftTooltipMapper.ShowTooltip(tooltip);
            rightTooltipMapper.ShowTooltip(tooltip);
        }
    }

    public void HideTooltip(Tooltip tooltip, bool remove=false)
    {
        if (remove)
        {
            if (tooltips.ContainsKey(tooltip.tooltipName))
            {
                tooltips.Remove(tooltip.tooltipName);
            }
        }

        if (tooltip.hand == TooltipHand.Left)
        {
            leftTooltipMapper.HideTooltip(tooltip);
        }
        else if (tooltip.hand == TooltipHand.Right)
        {
            rightTooltipMapper.HideTooltip(tooltip);
        }
        else if (tooltip.hand == TooltipHand.Both)
        {
            leftTooltipMapper.HideTooltip(tooltip);
            rightTooltipMapper.HideTooltip(tooltip);
        }
    }

    public void AddTooltip(Tooltip tooltip, bool show=true)
    {
        if (!tooltips.ContainsKey(tooltip.tooltipName))
        {
            tooltips.Add(tooltip.tooltipName, tooltip);
        }

        if (show && showTooltips)
        {
            if (tooltip.hand == TooltipHand.Left)
            {
                leftTooltipMapper.HideTooltip(tooltip);
            }
            else if (tooltip.hand == TooltipHand.Right)
            {
                rightTooltipMapper.HideTooltip(tooltip);
            }
            else if (tooltip.hand == TooltipHand.Both)
            {
                leftTooltipMapper.HideTooltip(tooltip);
                rightTooltipMapper.HideTooltip(tooltip);
            }
        }
    }

    public void RemoveTooltip(Tooltip tooltip)
    {
        if (tooltips.ContainsKey(tooltip.tooltipName))
        {
            tooltips.Remove(tooltip.tooltipName);

            if (tooltip.hand == TooltipHand.Left)
            {
                leftTooltipMapper.HideTooltip(tooltip);
            }
            else if (tooltip.hand == TooltipHand.Right)
            {
                rightTooltipMapper.HideTooltip(tooltip);
            }
            else if (tooltip.hand == TooltipHand.Both)
            {
                leftTooltipMapper.HideTooltip(tooltip);
                rightTooltipMapper.HideTooltip(tooltip);
            }
        }
    }
}
