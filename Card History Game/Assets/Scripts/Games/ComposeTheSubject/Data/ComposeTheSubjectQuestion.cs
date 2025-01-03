using System;
using System.Collections.Generic;
using Games.ComposeTheSubject.Enums;

namespace Games.ComposeTheSubject.Data
{
    [Serializable]
    public class ComposeTheSubjectQuestion
    {
        public string Text;
        public List<SubjectType> Answers;
    }
}