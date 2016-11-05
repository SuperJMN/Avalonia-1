// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

namespace Avalonia.Markup.Xaml.Context
{
    using OmniXamlV1;
    using OmniXamlV1.ObjectAssembler;
    using OmniXamlV1.Parsers.Parser;
    using OmniXamlV1.Parsers.ProtoParser;
 
    public class AvaloniaParserFactory : IParserFactory
    {
        private readonly IRuntimeTypeSource runtimeTypeSource;

        public AvaloniaParserFactory()
            : this(new TypeFactory())
        {
        }

        public AvaloniaParserFactory(ITypeFactory typeFactory)
        {
            runtimeTypeSource = new AvaloniaRuntimeTypeSource(typeFactory);
        }      

        public IParser Create(Settings settings)
        {
            var xamlInstructionParser = new OrderAwareInstructionParser(new InstructionParser(runtimeTypeSource));

            IObjectAssembler objectAssembler = new AvaloniaObjectAssembler(
                runtimeTypeSource,
                new TopDownValueContext(),
                settings);
            var phaseParserKit = new PhaseParserKit(
                new ProtoInstructionParser(runtimeTypeSource),
                xamlInstructionParser,
                objectAssembler);

            return new XmlParser(phaseParserKit);
        }
    }
}