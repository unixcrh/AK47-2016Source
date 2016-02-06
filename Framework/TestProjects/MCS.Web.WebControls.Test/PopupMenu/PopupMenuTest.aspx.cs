using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MCS.Web.WebControls.Test.PopupMenu
{
	public partial class PopupMenuTest : System.Web.UI.Page
	{
		#region 待测属性
		//ctrlPopupMenu.XLeft;					Delete	//(主控件)菜单弹出的位置 x坐标
		//ctrlPopupMenu.YTop;					Delete	//(主控件)菜单弹出的位置 y坐标
		//ctrlPopupMenu.Orientation;					//(主控件)一级菜单排列方向，默认值为Vertical
		//ctrlPopupMenu.IsImageIndent;					//(主控件)菜单条目前的图标是否缩进，默认为否
		//ctrlPopupMenu.MultiSelect;					//(主控件)是否是多选，默认为false
		//ctrlPopupMenu.HasControlSeparator;	New		//(主控件)是否处理分割线，默认为false
		//ctrlPopupMenu.StaticDisplayLevels;			//(主控件)静态菜单级别，默认为1
		//ctrlPopupMenu.SubMenuIndent;					//(主控件)静态菜单中，子菜单缩进长度，默认值为10
		//ctrlPopupMenu.TextHeadWidth;					//(主控件)菜单文本前的空格宽度，默认为5
		//ctrlPopupMenu.ItemFontWidth;			New		//(主控件)菜单项文字宽度，默认值为150
		//ctrlPopupMenu.Target;							//(主控件)[打开链接目标，默认为空]打开链接目标,打开目标的不同方式（_blank, _parent, _search, _self, _top）
		//ctrlPopupMenu.Items;							//(主控件)菜单条目集合
		//ctrlPopupMenu.OnMenuItemClick;				//(主控件)单击菜单项事件
		//ctrlPopupMenu.OnMenuItemShowen;		Delete	//(主控件)响应弹出动态菜单事件的客户端函数名称
		//ctrlPopupMenu.OnMenuPopup;			New		//(主控件)子菜单弹出事件
		//ctrlPopupMenu.DefaultPopOutImageUrl;	New		//(主控件)标示默认下一级动态菜单图标
		//ctrlPopupMenu.StaticPopOutImageUrl;			//(主控件)弹出静态菜单图标
		//ctrlPopupMenu.DynamicPopOutImageUrl;			//(主控件)弹出动态菜单图标
		//ctrlPopupMenu.CssClass;				Delete	//(主控件)菜单的CssClass
		//ctrlPopupMenu.ItemCssClass;					//(主控件)菜单条目的CssClass
		//ctrlPopupMenu.HoverItemCssClass;				//(主控件)鼠标移到菜单条目的CssClass
		//ctrlPopupMenu.SelectedItemCssClass;			//(主控件)菜单条目选择后的CssClass
		//ctrlPopupMenu.SeparatorCssClass;				//(主控件)菜单分割线的CssClass
		//ctrlPopupMenu.ImageColCssClass;				//(主控件)菜单条目前的图标表格CssClass

		//ctrlPopupMenu.GetMenuDesignHTML(ctrlPopupMenu);

		//ctrlPopupMenu.Items[0].Enable;		New		//(菜单项)该菜单项是否可用，默认为true
		//ctrlPopupMenu.Items[0].IsSeparator;	New		//(菜单项)是否是分隔线，默认为false
		//ctrlPopupMenu.Items[0].Visible;		New		//(菜单项)该菜单项是否可见，默认为true
		//ctrlPopupMenu.Items[0].Text;					//(菜单项)菜单条目文本
		//ctrlPopupMenu.Items[0].Value;					//(菜单项)菜单条目值
		//ctrlPopupMenu.Items[0].Target;				//(菜单项)[菜单条目链接目标，默认为空]菜单条目链接目标,打开目标的不同方式（_blank, _parent, _search, _self, _top）
		//ctrlPopupMenu.Items[0].NavigateUrl;			//(菜单项)菜单条目链接Url
		//ctrlPopupMenu.Items[0].ImageUrl;				//(菜单项)菜单条目前图标
		//ctrlPopupMenu.Items[0].ToolTip;				//(菜单项)菜单条目提示
		//ctrlPopupMenu.Items[0].Selected;				//(菜单项)菜单条目是否被选中,默认为false
		//ctrlPopupMenu.Items[0].SeparatorMode;			//(菜单项)菜单条目分割线样式,默认为PopupMenuItemSeparatorMode.None
		//ctrlPopupMenu.Items[0].Parent;				//(菜单项)菜单项的父项
		//ctrlPopupMenu.Items[0].ChildItems;			//(菜单项)菜单项的子项的集合
		//ctrlPopupMenu.Items[0].DynamicPopOutImageUrl;	//(菜单项)动态弹出菜单图标路径(不设置则使用默认图标)
		//ctrlPopupMenu.Items[0].StaticPopOutImageUrl;	//(菜单项)静态弹出菜单图标路径(不设置则使用默认图标)
		//ctrlPopupMenu.Items[0].Previous;				//(菜单项)菜单项的前一项
		//ctrlPopupMenu.Items[0].Next;					//(菜单项)菜单项的后一项
		//ctrlPopupMenu.Items[0].NodeID;				//(菜单项)节点ID
		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected void btnSetProperties_Click(object sender, EventArgs e)
		{
			ctrlPopupMenu.Orientation = (MenuOrientation)Enum.Parse(typeof(MenuOrientation), ddlOrientation.Text);
			ctrlPopupMenu.IsImageIndent = ckbIsImageIndent.Checked;
			ctrlPopupMenu.MultiSelect = ckbMultiSelect.Checked;
			ctrlPopupMenu.HasControlSeparator = ckbHasControlSeparator.Checked;
			ctrlPopupMenu.StaticDisplayLevels = Convert.ToInt32(txbStaticDisplayLevels.Text);
			ctrlPopupMenu.SubMenuIndent = Convert.ToInt32(txbSubMenuIndent.Text);
			ctrlPopupMenu.TextHeadWidth = Convert.ToInt32(txbTextHeadWidth.Text);
			ctrlPopupMenu.ItemFontWidth = Convert.ToInt32(txbItemFontWidth.Text);
			ctrlPopupMenu.Target = ddlTarget.Text;
			if (ddlStaticPopOutImageUrl.Text != "Default")
			{
				ctrlPopupMenu.StaticPopOutImageUrl = ddlStaticPopOutImageUrl.Text;
			}
			if (ddlDynamicPopOutImageUrl.Text != "Default")
			{
				ctrlPopupMenu.DynamicPopOutImageUrl = ddlDynamicPopOutImageUrl.Text;
			}
			if (ddlItemCssClass.Text != "Default")
			{
				ctrlPopupMenu.ItemCssClass = ddlItemCssClass.Text;
			}
			if (ddlHoverItemCssClass.Text != "Default")
			{
				ctrlPopupMenu.HoverItemCssClass = ddlHoverItemCssClass.Text;
			}
			if (ddlSelectedItemCssClass.Text != "Default")
			{
				ctrlPopupMenu.SelectedItemCssClass = ddlSelectedItemCssClass.Text;
			}
			if (ddlImageColCssClass.Text != "Default")
			{
				ctrlPopupMenu.ImageColCssClass = ddlImageColCssClass.Text;
			}

			//主控件属性复制到第二个菜单控件
			ctrlPopupMenu2.Orientation = ctrlPopupMenu.Orientation;
			ctrlPopupMenu2.IsImageIndent = ctrlPopupMenu.IsImageIndent;
			ctrlPopupMenu2.MultiSelect = ctrlPopupMenu.MultiSelect;
			ctrlPopupMenu2.HasControlSeparator = ctrlPopupMenu.HasControlSeparator;
			ctrlPopupMenu2.StaticDisplayLevels = ctrlPopupMenu.StaticDisplayLevels;
			ctrlPopupMenu2.SubMenuIndent = ctrlPopupMenu.SubMenuIndent;
			ctrlPopupMenu2.TextHeadWidth = ctrlPopupMenu.TextHeadWidth;
			ctrlPopupMenu2.ItemFontWidth = ctrlPopupMenu.ItemFontWidth;
			ctrlPopupMenu2.Target = ctrlPopupMenu.Target;
			ctrlPopupMenu2.StaticPopOutImageUrl = ctrlPopupMenu.StaticPopOutImageUrl;
			ctrlPopupMenu2.DynamicPopOutImageUrl = ctrlPopupMenu.DynamicPopOutImageUrl;
			ctrlPopupMenu2.ItemCssClass = ctrlPopupMenu.ItemCssClass;
			ctrlPopupMenu2.HoverItemCssClass = ctrlPopupMenu.HoverItemCssClass;
			ctrlPopupMenu2.SelectedItemCssClass = ctrlPopupMenu.SelectedItemCssClass;
			ctrlPopupMenu2.ImageColCssClass = ctrlPopupMenu.ImageColCssClass;
		}

		#region 设置节点（已取消）
		protected void btnSetMenuItem_Click(object sender, EventArgs e)
		{
			int[] intNodeID ={ 0, 0, 0, 0, 0 };
			string[] strTrans = txbItemNodeID.Text.Split(',');
			for (int i = 0; i < strTrans.Length; i++)
			{
				intNodeID[i] = Convert.ToInt32(strTrans[i]);
			}
			MenuItem transMenuItem = new MenuItem();
			switch (strTrans.Length)
			{
				case 0:
					break;
				case 1:
					transMenuItem = ctrlPopupMenu.Items[intNodeID[0] - 1];
					break;
				case 2:
					transMenuItem = ctrlPopupMenu.Items[intNodeID[0] - 1].ChildItems[intNodeID[1] - 1];
					break;
				case 3:
					transMenuItem = ctrlPopupMenu.Items[intNodeID[0] - 1].ChildItems[intNodeID[1] - 1].ChildItems[intNodeID[2] - 1];
					break;
				case 4:
					transMenuItem = ctrlPopupMenu.Items[intNodeID[0] - 1].ChildItems[intNodeID[1] - 1].ChildItems[intNodeID[2] - 1].ChildItems[intNodeID[3] - 1];
					break;
				case 5:
					transMenuItem = ctrlPopupMenu.Items[intNodeID[0] - 1].ChildItems[intNodeID[1] - 1].ChildItems[intNodeID[2] - 1].ChildItems[intNodeID[3] - 1].ChildItems[intNodeID[4] - 1];
					break;
			}
			transMenuItem.Enable = ckbItemEnable.Checked;
			transMenuItem.IsSeparator = ckbItemIsSeparator.Checked;
			transMenuItem.Visible = ckbItemVisible.Checked;
			transMenuItem.Selected = ckbItemSelected.Checked;
			transMenuItem.Text = txbItemText.Text;
			transMenuItem.Value = txbItemValue.Text;
			transMenuItem.Target = txbItemTarget.Text;
			transMenuItem.NavigateUrl = txbItemNavigateUrl.Text;
			transMenuItem.ImageUrl = txbItemImageUrl.Text;
			transMenuItem.ToolTip = txbItemToolTip.Text;
			transMenuItem.DynamicPopOutImageUrl = txbItemDynamicPopOutImageUrl.Text;
			transMenuItem.StaticPopOutImageUrl = txbItemStaticPopOutImageUrl.Text;
			txbItemNodeID.Enabled = true;
			btnGetMenuItem.Enabled = true;
			btnSetMenuItem.Enabled = false;

			txbItemNodeID.Text = string.Empty;
			txbItemText.Text = string.Empty;
			txbItemValue.Text = string.Empty;
			txbItemTarget.Text = string.Empty;
			txbItemNavigateUrl.Text = string.Empty;
			txbItemImageUrl.Text = string.Empty;
			ckbItemSelected.Checked = false;
			txbItemStaticPopOutImageUrl.Text = string.Empty;
			txbItemDynamicPopOutImageUrl.Text = string.Empty;
			txbItemToolTip.Text = string.Empty;
			txbItemPrevious.Text = string.Empty;
			txbItemNext.Text = string.Empty;
			txbItemParent.Text = string.Empty;
			textAreaChildren.Value = string.Empty;
		}
		#endregion

		protected void btnGetMenuItem_Click(object sender, EventArgs e)
		{
			int[] intNodeID ={ 0, 0, 0, 0, 0 };
			string[] strTrans = txbItemNodeID.Text.Split(',');
			for (int i = 0; i < strTrans.Length; i++)
			{
				intNodeID[i] = Convert.ToInt32(strTrans[i]);
			}
			MenuItem transMenuItem = new MenuItem();
			try
			{
				switch (strTrans.Length)
				{
					case 0:
						break;
					case 1:
						transMenuItem = ctrlPopupMenu.Items[intNodeID[0] - 1];
						break;
					case 2:
						transMenuItem = ctrlPopupMenu.Items[intNodeID[0] - 1].ChildItems[intNodeID[1] - 1];
						break;
					case 3:
						transMenuItem = ctrlPopupMenu.Items[intNodeID[0] - 1].ChildItems[intNodeID[1] - 1].ChildItems[intNodeID[2] - 1];
						break;
					case 4:
						transMenuItem = ctrlPopupMenu.Items[intNodeID[0] - 1].ChildItems[intNodeID[1] - 1].ChildItems[intNodeID[2] - 1].ChildItems[intNodeID[3] - 1];
						break;
					case 5:
						transMenuItem = ctrlPopupMenu.Items[intNodeID[0] - 1].ChildItems[intNodeID[1] - 1].ChildItems[intNodeID[2] - 1].ChildItems[intNodeID[3] - 1].ChildItems[intNodeID[4] - 1];
						break;
				}
				ckbItemEnable.Checked = transMenuItem.Enable;
				ckbItemIsSeparator.Checked = transMenuItem.IsSeparator;
				ckbItemVisible.Checked = transMenuItem.Visible;
				ckbItemSelected.Checked = transMenuItem.Selected;
				txbItemText.Text = transMenuItem.Text;
				txbItemValue.Text = transMenuItem.Value;
				txbItemTarget.Text = transMenuItem.Target;
				txbItemNavigateUrl.Text = transMenuItem.NavigateUrl;
				txbItemImageUrl.Text = transMenuItem.ImageUrl;
				txbItemStaticPopOutImageUrl.Text = transMenuItem.StaticPopOutImageUrl;
				txbItemDynamicPopOutImageUrl.Text = transMenuItem.DynamicPopOutImageUrl;
				txbItemToolTip.Text = transMenuItem.ToolTip;
				if (transMenuItem.Previous == null)
				{
					txbItemPrevious.Text = "No Previous MenuItem";
				}
				else
				{
					txbItemPrevious.Text = transMenuItem.Previous.Text;
				}
				if (transMenuItem.Next == null)
				{
					txbItemNext.Text = "No Next MenuItem";
				}
				else
				{
					txbItemNext.Text = transMenuItem.Next.Text;
				}
				if (transMenuItem.Parent == null)
				{
					txbItemParent.Text = "No Parent MenuItem";
				}
				else
				{
					txbItemParent.Text = transMenuItem.Parent.Text;
				}
				if (transMenuItem.ChildItems.Count > 0)
				{
					StringBuilder stringbTrans = new StringBuilder(512);
					stringbTrans.Append(transMenuItem.ChildItems[0].Text);
					if (transMenuItem.ChildItems.Count > 1)
					{
						for (int i = 1; i < transMenuItem.ChildItems.Count; i++)
						{
							stringbTrans.Append("\n" + transMenuItem.ChildItems[i].Text);
						}
					}
					textAreaChildren.Value = stringbTrans.ToString();
				}
				else
				{
					textAreaChildren.Value = "No Children MenuItems";
				}
				txbItemNodeID.Enabled = false;
				btnGetMenuItem.Enabled = false;
				btnSetMenuItem.Enabled = true;
				LiteralJS.Text = string.Empty;
			}
			catch
			{
				txbItemNodeID.Text = string.Empty;
				LiteralJS.Text = "<script>alertJS();</script>";
				txbItemText.Text = string.Empty;
				txbItemValue.Text = string.Empty;
				txbItemTarget.Text = string.Empty;
				txbItemNavigateUrl.Text = string.Empty;
				txbItemImageUrl.Text = string.Empty;
				ckbItemSelected.Checked = false;
				txbItemStaticPopOutImageUrl.Text = string.Empty;
				txbItemDynamicPopOutImageUrl.Text = string.Empty;
				txbItemToolTip.Text = string.Empty;
				txbItemPrevious.Text = string.Empty;
				txbItemNext.Text = string.Empty;
				txbItemParent.Text = string.Empty;
				textAreaChildren.Value = string.Empty;
			}
		}
	}
}
