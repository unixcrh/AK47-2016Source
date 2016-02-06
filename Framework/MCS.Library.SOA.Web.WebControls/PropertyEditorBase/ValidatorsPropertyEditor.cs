using System.Web.UI;

namespace MCS.Web.WebControls
{
    /// <summary>
    /// 属性值校验器编辑器
    /// </summary>
    public class ValidatorsPropertyEditor : PropertyEditorBase
    {  
        public override bool IsCloneControlEditor
        {
            get
            {
                return true;
            }
        }

        public override string DefaultCloneControlName()
        {
            return "ValidatorsPropertyEditor_ValidatorSelectorControl";
        }

        public override Control CreateDefalutControl()
        {
            return new ValidatorSelectorControl() { ID = this.DefaultCloneControlName() };
        }

        //protected internal override void OnPagePreInit(Page page)
        //{
        //    CreateControls(page);
        //}

        protected internal override void OnPageInit(Page page)
        {
            //Callback时，提前创建模版控件，拦截请求
            if (page.IsCallback)
                CreateControls(page);
        }

        protected internal override void OnPagePreRender(Page page)
        {
            //除了CallBack，创建模版控件在LoadViewState之后，避免ViewState混乱
            if (!page.IsCallback)
                CreateControls(page);
        }
    }
}