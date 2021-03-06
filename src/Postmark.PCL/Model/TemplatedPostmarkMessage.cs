﻿using Newtonsoft.Json.Linq;

namespace PostmarkDotNet
{
    /// <summary>
    /// Send a message using a template that you have previously created in Postmark.
    /// </summary>
    public class TemplatedPostmarkMessage : PostmarkMessageBase
    {
        private bool _inlineCss = true;
        public TemplatedPostmarkMessage()
            : base()
        {
        }

        /// <summary>
        /// Should the CSS in the HtmlBody of the template be inlined? Defaults to true.
        /// </summary>
        public bool InlineCss { get { return _inlineCss; } set { _inlineCss = value; } }

        /// <summary>
        /// The template to use when sending this message.
        /// </summary>
        public long TemplateId { get; set; }

        private object _templateModel;

        /// <summary>
        /// The values to merge with the template when creating the content.
        ///
        /// The message sending API requires that this be an *object* as described by the JSON specification.
        ///
        /// This means that you can assign a Dictionary&lt;K,V&gt; to this property. 
        /// The dictionary may contain any keys and or objects that can be serialized to JSON.
        /// 
        /// Additionally, POCOs and anonymous types can be assigned to this property, 
        /// provided they can be serialized to JSON (we use JSON.net internally).
        ///
        /// Objects that would be serialized as "JSON scalars" or arrays should *not* be assigned to this property.
        /// (They MAY be set as values on the TemplateModel, though)
        /// 
        /// See this guide for more information on how this model is used, and how Postmark Templates work:
        /// http://support.postmarkapp.com/article/786-using-a-postmark-starter-template
        /// </summary>
        /// <remarks>
        /// In some cases (perhaps for performance reasons), you may wish to assign a "pre-serialized" 
        /// JSON object to this property. In such cases, you can assign it as a string. Keep in mind that 
        /// the string MUST a JSON Object, not a scalar or array, and if you do assign an invalid model, 
        /// the API may reject it.
        /// </remarks>
        public object TemplateModel {
            get
            {
                if (_templateModel is JRaw)
                {
                    return ((JRaw)_templateModel).Value;
                }
                else
                {
                    return _templateModel;
                }
            }
            set
            {
                if(value is string)
                {
                    _templateModel = new JRaw(value);
                }
                else
                {
                    _templateModel = value;
                }
            }
        }
    }
}
