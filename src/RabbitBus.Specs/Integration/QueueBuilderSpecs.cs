﻿using System.Collections.Generic;
using Machine.Specifications;
using RabbitBus.Specs.Infrastructure;
using RabbitBus.Utilities;
using RabbitMQ.Client;

namespace RabbitBus.Specs.Integration
{
	[Integration]
	[Subject("Queues")]
	public class when_creating_a_queue
	{
		const string SpecId = "875C047E-BAD2-4A58-BC92-1DAAD2D6B0AA";
		static bool _queueExists;

		Establish context = () => Declare.Queue(SpecId, ctx =>
		                                                ctx.Not.AutoDelete()
														.WithRoutingKey(string.Empty)
		                                                   .WithExchange(SpecId));

		Cleanup after = () => new RabbitQueue("localhost", SpecId, ExchangeType.Direct, SpecId, true, false, true, false).Delete().Close();

		Because of = () => _queueExists = RabbitQueue.QueueExists(SpecId);

		It should_create_the_queue = () => _queueExists.ShouldBeTrue();
	}

	[Integration]
	[Subject("Queues")]
	public class when_creating_a_queue_with_headers
	{
		const string SpecId = "1EE32634-6E04-49FC-9CCA-08A62E515D89";
		static bool _queueExists;

		Establish context = () => Declare.Queue(SpecId, ctx =>
		                                                ctx.Not.AutoDelete()
		                                                   .WithHeaders(new Dictionary<string, object>())
		                                                   .WithExchange(SpecId));

		Because of = () => _queueExists = RabbitQueue.QueueExists(SpecId);

		Cleanup after = () => new RabbitQueue("localhost", SpecId, ExchangeType.Direct, SpecId, true, false, true, false).Delete().Close();

		It should_create_the_queue = () => _queueExists.ShouldBeTrue();
	}
}