using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SCSIApplication
{
    class UIHandler
    {
        #region Control
        delegate void setContorlEnabledCallback(Control control, bool enabled);
        public static void SetControlEnabled(Control control, bool enabled)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new setContorlEnabledCallback(SetControlEnabled), control, enabled);
            }
            else
            {
                control.Enabled = enabled;
            }
        }
        #endregion

        #region Label
        delegate void setLabelTextCallback(Label label, string text);
        public static void SetLabelText(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                label.Invoke(new setLabelTextCallback(SetLabelText), label, text);
            }
            else
            {
                label.Text = text;
            }
        }
        #endregion

        #region TextBox
        delegate string getTextBoxTextCallback(TextBox textBox);
        public static string GetTextBoxText(TextBox textBox)
        {
            string text = null;
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new getTextBoxTextCallback(GetTextBoxText), textBox);
            }
            else
            {
                text = textBox.Text;
            }
            return text;
        }

        delegate void setTextBoxTextCallback(TextBox textBox, string text);
        public static void SetTextBoxText(TextBox textBox, string text)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new setTextBoxTextCallback(SetTextBoxText), textBox, text);
            }
            else
            {
                textBox.Text = text;
            }
        }
        #endregion

        #region CheckBox
        delegate bool getCheckBoxCheckedCallback(CheckBox checkBox);
        public static bool GetCheckBoxChecked(CheckBox checkBox)
        {
            bool chked = false;
            if (checkBox.InvokeRequired)
            {
                checkBox.Invoke(new getCheckBoxCheckedCallback(GetCheckBoxChecked), checkBox);
            }
            else
            {
                chked = checkBox.Checked;
            }
            return chked;
        }

        delegate void setCheckBoxCheckedCallback(CheckBox checkBox, bool chked);
        public static void SetCheckBoxChecked(CheckBox checkBox, bool chked)
        {
            if (checkBox.InvokeRequired)
            {
                checkBox.Invoke(new setCheckBoxCheckedCallback(SetCheckBoxChecked), checkBox, chked);
            }
            else
            {
                checkBox.Checked = chked;
            }
        }
        #endregion

        #region ComboBox
        /*delegate int getComboBoxSelectedIndexCallback(ComboBox comboBox);
        public static int GetComboBoxSelectedIndex(ComboBox comboBox)
        {
            int index = 0;
            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(new getComboBoxSelectedIndexCallback(GetComboBoxSelectedIndex), comboBox);
            }
            else
            {
                comboBox.DataSource = null;
                comboBox.Text = "";
            }
            return index;
        }*/

        delegate void setComboBoxSelectedIndexCallback(ComboBox comboBox, int index);
        public static void SetComboBoxSelectedIndex(ComboBox comboBox, int index)
        {
            if (index >= comboBox.Items.Count || index < 0) return;
            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(new setComboBoxSelectedIndexCallback(SetComboBoxSelectedIndex), comboBox, index);
            }
            else
            {
                comboBox.SelectedIndex = index;
            }
        }

        delegate void setComboBoxValueCallback(ComboBox comboBox, List<string> deviceList);
        public static void setComboBoxValue(ComboBox comboBox, List<string> deviceList)
        {
            //if (deviceList.Count <= 0) return;
            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(new setComboBoxValueCallback(setComboBoxValue), comboBox, deviceList);
            }
            else
            {
                comboBox.DataSource = deviceList;
            }
        }

        delegate void cleanComboBoxValueCallback(ComboBox comboBox);
        public static void cleanComboBoxValue(ComboBox comboBox)
        {
            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(new cleanComboBoxValueCallback(cleanComboBoxValue), comboBox);
            }
            else
            {
                comboBox.DataSource = null;
                comboBox.Text = "";
            }
        }

        #endregion

        #region Panel
        delegate void setPanelBackColorCallback(Panel panel, Color color);
        public static void SetPanelBackColor(Panel panel, Color color)
        {
            if (panel.InvokeRequired)
            {
                panel.Invoke(new setPanelBackColorCallback(SetPanelBackColor), panel, color);
            }
            else
            {
                panel.BackColor = color;
            }
        }
        #endregion
    }
}
