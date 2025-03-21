// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: game_content_game.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Muziverse.Proto.GameContent.Api.Game {
  public static partial class GameService
  {
    static readonly string __ServiceName = "gamecontent.game.GameService";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Muziverse.Proto.GameContent.Api.Game.GetGameRequest> __Marshaller_gamecontent_game_GetGameRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Muziverse.Proto.GameContent.Api.Game.GetGameRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Muziverse.Proto.GameContent.Domain.GameResponse> __Marshaller_GameResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Muziverse.Proto.GameContent.Domain.GameResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Google.Protobuf.WellKnownTypes.Empty> __Marshaller_google_protobuf_Empty = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Google.Protobuf.WellKnownTypes.Empty.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Muziverse.Proto.GameContent.Api.Game.GetListGameResponse> __Marshaller_gamecontent_game_GetListGameResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Muziverse.Proto.GameContent.Api.Game.GetListGameResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Muziverse.Proto.GameContent.Api.Game.CreateGameRequest> __Marshaller_gamecontent_game_CreateGameRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Muziverse.Proto.GameContent.Api.Game.CreateGameRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Muziverse.Proto.GameContent.Api.Game.UpdateGameRequest> __Marshaller_gamecontent_game_UpdateGameRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Muziverse.Proto.GameContent.Api.Game.UpdateGameRequest.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Muziverse.Proto.GameContent.Api.Game.GetGameRequest, global::Muziverse.Proto.GameContent.Domain.GameResponse> __Method_GetGameBySymbol = new grpc::Method<global::Muziverse.Proto.GameContent.Api.Game.GetGameRequest, global::Muziverse.Proto.GameContent.Domain.GameResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetGameBySymbol",
        __Marshaller_gamecontent_game_GetGameRequest,
        __Marshaller_GameResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::Muziverse.Proto.GameContent.Api.Game.GetListGameResponse> __Method_GetListGames = new grpc::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::Muziverse.Proto.GameContent.Api.Game.GetListGameResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetListGames",
        __Marshaller_google_protobuf_Empty,
        __Marshaller_gamecontent_game_GetListGameResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Muziverse.Proto.GameContent.Api.Game.CreateGameRequest, global::Google.Protobuf.WellKnownTypes.Empty> __Method_CreateGame = new grpc::Method<global::Muziverse.Proto.GameContent.Api.Game.CreateGameRequest, global::Google.Protobuf.WellKnownTypes.Empty>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CreateGame",
        __Marshaller_gamecontent_game_CreateGameRequest,
        __Marshaller_google_protobuf_Empty);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Muziverse.Proto.GameContent.Api.Game.UpdateGameRequest, global::Google.Protobuf.WellKnownTypes.Empty> __Method_UpdateGame = new grpc::Method<global::Muziverse.Proto.GameContent.Api.Game.UpdateGameRequest, global::Google.Protobuf.WellKnownTypes.Empty>(
        grpc::MethodType.Unary,
        __ServiceName,
        "UpdateGame",
        __Marshaller_gamecontent_game_UpdateGameRequest,
        __Marshaller_google_protobuf_Empty);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Muziverse.Proto.GameContent.Api.Game.GameContentGameReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of GameService</summary>
    [grpc::BindServiceMethod(typeof(GameService), "BindService")]
    public abstract partial class GameServiceBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Muziverse.Proto.GameContent.Domain.GameResponse> GetGameBySymbol(global::Muziverse.Proto.GameContent.Api.Game.GetGameRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Muziverse.Proto.GameContent.Api.Game.GetListGameResponse> GetListGames(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Google.Protobuf.WellKnownTypes.Empty> CreateGame(global::Muziverse.Proto.GameContent.Api.Game.CreateGameRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Google.Protobuf.WellKnownTypes.Empty> UpdateGame(global::Muziverse.Proto.GameContent.Api.Game.UpdateGameRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for GameService</summary>
    public partial class GameServiceClient : grpc::ClientBase<GameServiceClient>
    {
      /// <summary>Creates a new client for GameService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public GameServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for GameService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public GameServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected GameServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected GameServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Muziverse.Proto.GameContent.Domain.GameResponse GetGameBySymbol(global::Muziverse.Proto.GameContent.Api.Game.GetGameRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetGameBySymbol(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Muziverse.Proto.GameContent.Domain.GameResponse GetGameBySymbol(global::Muziverse.Proto.GameContent.Api.Game.GetGameRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetGameBySymbol, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Muziverse.Proto.GameContent.Domain.GameResponse> GetGameBySymbolAsync(global::Muziverse.Proto.GameContent.Api.Game.GetGameRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetGameBySymbolAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Muziverse.Proto.GameContent.Domain.GameResponse> GetGameBySymbolAsync(global::Muziverse.Proto.GameContent.Api.Game.GetGameRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetGameBySymbol, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Muziverse.Proto.GameContent.Api.Game.GetListGameResponse GetListGames(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetListGames(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Muziverse.Proto.GameContent.Api.Game.GetListGameResponse GetListGames(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetListGames, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Muziverse.Proto.GameContent.Api.Game.GetListGameResponse> GetListGamesAsync(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetListGamesAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Muziverse.Proto.GameContent.Api.Game.GetListGameResponse> GetListGamesAsync(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetListGames, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Google.Protobuf.WellKnownTypes.Empty CreateGame(global::Muziverse.Proto.GameContent.Api.Game.CreateGameRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CreateGame(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Google.Protobuf.WellKnownTypes.Empty CreateGame(global::Muziverse.Proto.GameContent.Api.Game.CreateGameRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_CreateGame, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> CreateGameAsync(global::Muziverse.Proto.GameContent.Api.Game.CreateGameRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CreateGameAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> CreateGameAsync(global::Muziverse.Proto.GameContent.Api.Game.CreateGameRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_CreateGame, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Google.Protobuf.WellKnownTypes.Empty UpdateGame(global::Muziverse.Proto.GameContent.Api.Game.UpdateGameRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return UpdateGame(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Google.Protobuf.WellKnownTypes.Empty UpdateGame(global::Muziverse.Proto.GameContent.Api.Game.UpdateGameRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_UpdateGame, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> UpdateGameAsync(global::Muziverse.Proto.GameContent.Api.Game.UpdateGameRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return UpdateGameAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> UpdateGameAsync(global::Muziverse.Proto.GameContent.Api.Game.UpdateGameRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_UpdateGame, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override GameServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new GameServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(GameServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_GetGameBySymbol, serviceImpl.GetGameBySymbol)
          .AddMethod(__Method_GetListGames, serviceImpl.GetListGames)
          .AddMethod(__Method_CreateGame, serviceImpl.CreateGame)
          .AddMethod(__Method_UpdateGame, serviceImpl.UpdateGame).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, GameServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_GetGameBySymbol, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Muziverse.Proto.GameContent.Api.Game.GetGameRequest, global::Muziverse.Proto.GameContent.Domain.GameResponse>(serviceImpl.GetGameBySymbol));
      serviceBinder.AddMethod(__Method_GetListGames, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Google.Protobuf.WellKnownTypes.Empty, global::Muziverse.Proto.GameContent.Api.Game.GetListGameResponse>(serviceImpl.GetListGames));
      serviceBinder.AddMethod(__Method_CreateGame, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Muziverse.Proto.GameContent.Api.Game.CreateGameRequest, global::Google.Protobuf.WellKnownTypes.Empty>(serviceImpl.CreateGame));
      serviceBinder.AddMethod(__Method_UpdateGame, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Muziverse.Proto.GameContent.Api.Game.UpdateGameRequest, global::Google.Protobuf.WellKnownTypes.Empty>(serviceImpl.UpdateGame));
    }

  }
}
#endregion
